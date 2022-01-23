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
    [Authorize(Roles = "User")] // only logged Client can do these actions below
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
            if (_clientService.AddToBasket(id, bikeItem) == (true, "Item added to Your basket"))
            {
                return Ok("Item added to Your Basket");
            }
            else if (_clientService.AddToBasket(id, bikeItem) == (false, "Wrong bike's id"))
            {
                return BadRequest("Wrong bike's id");
            }
            else
            {
                return BadRequest("Wrong Client's id");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFromBasket(long id, [FromBody] ItemDto bikeItem)
        {
            var result = _clientService.DeleteFromBasket(id, bikeItem);
            if (result.Item1 == true)
            {
                return Ok("Item deleted from Your Basket");
            }
            else
            {
                return BadRequest(result.Item2);
            }
            
        }

        [HttpGet("{id}")]
        public ClientDetailsDto GetDetails(long id)
        {
            return _clientService.GetDetails(id);
        }
    }
}
