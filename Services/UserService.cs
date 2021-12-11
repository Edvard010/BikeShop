using BikeShop.Dto;
using BikeShop.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace BikeShop.Services
{
    public class UserService
    {
        private readonly BikeShopContext _context;
        private readonly IConfiguration _config;

        public void Register(RegisterDto register)
        {
            var user = new User
            {
                Login = register.Login,
                Password = GetHash(register.Password)
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
