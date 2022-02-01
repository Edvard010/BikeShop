using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Dto
{
    public class BikeDetailsDto
    {
        public long Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public long ShopId { get; set; }
        
    }
}
