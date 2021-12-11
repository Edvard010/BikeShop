using BikeShop.Dto;
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
        [HttpGet("{id}")]
        public ShopDetailsDto GetDetails(long id)
        {
            var details = new ShopDetailsDto 
            {
                Id = id,
                Name = "testowe nejm"
            };
            return details;
        }

        [HttpPost]
        public ShopDetailsDto CreateShop([FromBody]ShopDetailsDto shop)
        {

            return shop;
        }
    }
}
