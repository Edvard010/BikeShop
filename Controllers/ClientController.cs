using BikeShop.Dto;
using BikeShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BikeShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[Authorize(Roles = "User")] // only logged Client can do these actions below
    public class ClientController : ControllerBase
    {
        private ClientService _clientService;
        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost("{id}")]
        public IActionResult AddToBasket(long id, [FromBody] ItemDto bikeItem)
        {
            _clientService.AddToBasket(id, bikeItem);
            return Ok("Item added to Your Basket");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFromBasket(long id, [FromBody] ItemDto bikeItem)
        {
            _clientService.DeleteFromBasket(id, bikeItem);
            return Ok("Item deleted from Your Basket");
        }

        [HttpGet("{id}")]
        public ClientDetailsDto GetDetails(long id)
        {
            return _clientService.GetDetails(id);
        }
    }
}
