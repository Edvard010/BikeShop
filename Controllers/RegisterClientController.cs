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
    public class RegisterClientController : ControllerBase
    {
        private ClientService _clientService;
        public RegisterClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public void Register(ClientRegisterDto register)
        {
            _clientService.Register(register);
        }
    }
}
