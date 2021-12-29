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
    public class ShopController : ControllerBase
    {
        private ShopService _shopService;
        public ShopController(ShopService shopService)
        {
            _shopService = shopService;
        }


        [HttpGet("{id}")]
        public ShopDetailsDto GetDetails(long id)
        {
            return _shopService.GetDetails(id);            
        }

        [HttpPost]
        public IActionResult CreateShop(NewShopDto shop)
        {
            if (_shopService.Create(shop) == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Shop with this address already exists");
            }            
        }

        [HttpGet]
        public IEnumerable<ShopItemDto> GetAll()
        {
            return _shopService.GetAll();
        }

        [HttpPut("{id}")]
        public IActionResult EditShop(long id, [FromBody] ShopDetailsDto shopChanges)
        {
            if (_shopService.EditShop(id, shopChanges) == true)
            {
                return Ok("Shop's data Edited");
            }
            else
            {
                return BadRequest("Wrong shop's id");
            }

        }
    }
}
