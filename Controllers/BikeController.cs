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

        [HttpGet]
        public IEnumerable<BikeItemDto> GetAll()
        {
            return _bikeService.GetAll();
        }
    }
}
