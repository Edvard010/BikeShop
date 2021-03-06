using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Model
{
    public class Shop
    {
        public Shop()
        {
            Bikes = new Collection<Bike>();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public ICollection<Bike> Bikes { get; set; }

    }
}
