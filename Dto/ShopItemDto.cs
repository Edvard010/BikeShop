using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Dto
{
    public class ShopItemDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public IEnumerable<BikeItemDto> Bikes { get; set; }
        //public int Bikes { get; set; }
    }
}
