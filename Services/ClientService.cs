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

        public void AddToBasket(long id, ItemDto bikeItem)
        {
            var client = _context.Clients.SingleOrDefault(x => x.Id == id);
            if (client != null)
            {
                var id2 = bikeItem.Id;
                var bike = _context.Bikes.SingleOrDefault(x => x.Id == id2);
                client.Basket.Add(bike);
                
            }
            else
            {
                return;
            }                        
            _context.SaveChanges();
        }

        public void DeleteFromBasket(long id, ItemDto bikeItem)
        {
            var client = _context.Clients.SingleOrDefault(x => x.Id == id);
            if (client != null)
            {
                var id2 = bikeItem.Id;
                var bike = _context.Bikes.SingleOrDefault(x => x.Id == id2);
                client.Basket.Remove(bike);
                
            }
            else
            {
                return;
            }
            _context.SaveChanges();
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
    }
}
