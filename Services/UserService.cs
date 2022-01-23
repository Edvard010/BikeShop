using BikeShop.Dto;
using BikeShop.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace BikeShop.Services
{
    public class UserService
    {
        private readonly BikeShopContext _context;
        private readonly IConfiguration _config;
        public UserService(BikeShopContext bikeShopContext, IConfiguration configuration)
        {
            _config = configuration;
            _context = bikeShopContext;
        }
        public (bool, UserExistsDto) UserExists(UserExistsDto login)
        {
            var user = _context.Users.SingleOrDefault(x => x.Login == login.Login);
            if (user != null)
            {
                var userExistsDto = new UserExistsDto
                {
                    Login = user.Login
                };
                return (true, userExistsDto);
            }
            else
            {
                return (false, null);
            }
        }

        public (bool, LoginDto) Login(LoginDto login)
        {
            var hash = GetHash(login.Password);

            var user = _context.Users.SingleOrDefault(x => x.Login == login.Login);
            if (user != null)
            {
                if (user.Password == hash)
                {
                    var loginDto = new LoginDto
                    {
                        Login = user.Login,
                        Token = Authenticate(user.Login)
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

        public void Register(RegisterDto register)
        {
            var user = new User
            {
                Login = register.Login,
                Password = GetHash(register.Password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();
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
            if (!_context.Users.Any(u => u.Login == login))
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
                    new Claim(ClaimTypes.Role, "Admin")
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
