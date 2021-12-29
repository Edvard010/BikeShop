using BikeShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Dto
{
    public class ClientDetailsDto
    {
        public long Id { get; set; }
        public string Login { get; set; }
        
        public IEnumerable<BikeItemDto> Basket { get; set; }
    }
}
