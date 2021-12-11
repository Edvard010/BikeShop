using BikeShop.Dto;
using BikeShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private UserService _userService;
        public RegisterController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public void Register(RegisterDto register)
        {
            _userService.Register(register);
        }
    }
}
