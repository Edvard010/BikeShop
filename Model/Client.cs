using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Model
{
    public class Client
    {
        public Client()
        {
            Basket = new Collection<Bike>();
        }
        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public ICollection<Bike> Basket { get; set; }
    }
}
