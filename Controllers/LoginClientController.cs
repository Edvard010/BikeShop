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
    public class LoginClientController : ControllerBase
    {
        private ClientService _clientService;
        public LoginClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public IActionResult Login(ClientLoginDto login)
        {
            var (success, loginDto) = _clientService.Login(login);
            if (success)
            {
                return Ok(loginDto);
            }
            else
            {
                return BadRequest("login or password incorrect");
            }

        }
    }
}
