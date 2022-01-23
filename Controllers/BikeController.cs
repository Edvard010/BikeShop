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
    [Authorize(Roles = "Admin")] // only logged User can manage bikes in shops
    public class BikeController : ControllerBase
    {
        private BikeService _bikeService;
        public BikeController(BikeService bikeService)
        {
            _bikeService = bikeService;
        }

        [HttpGet("{id}")]
        public BikeDetailsDto GetDetails(long id)
        {
            return _bikeService.GetDetails(id);
        }

        
        [HttpPost]
        public NewBikeDto AddBike(NewBikeDto bike)
        {
            return _bikeService.AddBike(bike);
        }

        [HttpPut("{id}")]
        public IActionResult EditBike(long id, [FromBody]BikeDetailsDto bikeChanges)
        {
            if (_bikeService.EditBike(id, bikeChanges) == true)
            {
                return Ok("Edited");
            }
            else
            {
                return BadRequest("Wrong bike's id");
            }
            
        }
        
        [HttpGet]
        public IEnumerable<BikeItemDto> GetAll()
        {
            return _bikeService.GetAll();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBike(long id)
        {
            _bikeService.DeleteBike(id);
            return Ok("bike deleted from shop");
        }
    }
}
