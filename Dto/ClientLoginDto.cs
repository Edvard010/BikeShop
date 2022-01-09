using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Dto
{
    public class ClientLoginDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
