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
    [Route("api/Admin/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private UserService _userService;
        public LoginController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Login(LoginDto login)
        {
            var (success, loginDto) = _userService.Login(login);
            if (success)
            {
                return Ok(loginDto);
            }
            else
            {
                return BadRequest("login or password incorrect");
            }

        }

        [HttpGet("exists")]
        public IActionResult Exists(UserExistsDto login)
        {
            var (success, userExistsDto) = _userService.UserExists(login);
            if (success)
            {
                return Ok(userExistsDto);
            }
            else
            {
                return BadRequest("user doesn't exist!");
            }
        }
    }
}
