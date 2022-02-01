using BikeShop.Dto;
using BikeShop.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace BikeShop.Services
{
    public class ClientService
    {
        private readonly BikeShopContext _context;
        private readonly IConfiguration _config;
        public ClientService(BikeShopContext bikeShopContext, IConfiguration configuration)
        {
            _config = configuration;
            _context = bikeShopContext;
        }

        public (bool, ClientLoginDto) Login(ClientLoginDto login)
        {
            var hash = GetHash(login.Password);

            var client = _context.Clients.SingleOrDefault(x => x.Login == login.Login);
            if (client != null)
            {
                if (client.Password == hash)
                {
                    var loginDto = new ClientLoginDto
                    {
                        Login = client.Login,
                        Token = Authenticate(client.Login)
                    };
                    return (true, loginDto);
                }
                else
                {
                    return (false, null);
                }
            }
            else
            {
                return (false, null);
            }
        }


        public void Register(ClientRegisterDto register)
        {
            var client = new Client
            {
                Login = register.Login,
                Password = GetHash(register.Password),

            };

            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        public (bool, string) AddToBasket(long id, ItemDto bikeItem)
        {
            var client = _context.Clients.SingleOrDefault(x => x.Id == id);
            if (client != null)
            {
                var id2 = bikeItem.Id;
                var bike = _context.Bikes.SingleOrDefault(x => x.Id == id2); //dodać sprawdzanie czy ClientId == null, czyli jest wolny
                if (bike != null) //jak sprawdzić czy ClientId = null, jak w Client nie mam property ClientId, to się samo tutaj dodaje
                {
                    client.Basket.Add(bike);
                    _context.SaveChanges();
                    return (true, "Item added to Your basket");
                }
                else
                {
                    return (false, "Wrong bike's id");
                }
            }
            else
            {
                return (false, "Wrong Client's id");
            }
        }

        public (bool, string) DeleteFromBasket(long id, ItemDto bikeItem) 
        {
            var client = _context.Clients.SingleOrDefault(x => x.Id == id);
            if (client == null)
            {
                return (false, "Wrong Client's id");
            }
            var id2 = bikeItem.Id;
            var bike = _context.Bikes.SingleOrDefault(x => x.Id == id2);
            if (bike == null)
            {
                return (false, "Wrong Bike's id");
            }
            client.Basket.Remove(bike);
            _context.SaveChanges();
            return (true, "Succesfully deleted from Basket");
        }

        public ClientDetailsDto GetDetails(long id)
        {
            var client = _context.Clients.Include(x => x.Basket).SingleOrDefault(x => x.Id == id);

            if (client == null)
            {
                return null;
            }
            return new ClientDetailsDto
            {
                Id = client.Id,
                Login = client.Login,
                Basket = client.Basket.Select(p => new BikeItemDto
                {
                    Id = p.Id,
                    Brand = p.Brand,
                    Model = p.Model,
                    Size = p.Size,
                    Description = p.Description,
                    Price = p.Price
                })
            };
        }


        private string GetHash(string password)
        {
            var algorythm = SHA256.Create();

            StringBuilder sb = new StringBuilder();
            foreach (var b in algorythm.ComputeHash(Encoding.UTF8.GetBytes(password)))
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }

        public string Authenticate(string login)
        {
            if (!_context.Clients.Any(u => u.Login == login))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("TokenKey"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, login),
                    new Claim(ClaimTypes.Role, "User")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
