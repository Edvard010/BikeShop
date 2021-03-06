using BikeShop.Dto;
using BikeShop.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Services
{
    public class BikeService
    {
        private readonly BikeShopContext _context;
        private readonly IConfiguration _config;
        public BikeService(BikeShopContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        public NewBikeDto AddBike(NewBikeDto bike)
        {
            var newBike = new Bike
            {
                Brand = bike.Brand,
                Model = bike.Model,
                Size = bike.Size,
                Description = bike.Description,
                Price = bike.Price,
                ShopId = bike.ShopId
            };
            var shop = _context.Shops.SingleOrDefault(x => x.Id == bike.ShopId);
            if (shop == null)
            {
                return null;                
            }
            shop.Bikes.Add(newBike);
            _context.SaveChanges();
            return bike; 
        }

        public bool EditBike(long id, BikeDetailsDto bikeChanges)
        {
            var bike = _context.Bikes.SingleOrDefault(x => x.Id == id);
            if (bike != null)
            {
                {
                    bike.Brand = bikeChanges.Brand;
                    bike.Model = bikeChanges.Model;
                    bike.Description = bikeChanges.Description;
                    bike.Price = bikeChanges.Price;
                    bike.Size = bikeChanges.Size;
                }
                _context.SaveChanges();
                return true;                
            }            
            else
            {
                return false;
            }               
        }

        public IEnumerable<BikeItemDto> GetAll()
        {
            return _context.Bikes.Include(x => x.ShopId).ToList().Select(x => new BikeItemDto
            {
                Id = x.Id,
                Brand = x.Brand,
                Model = x.Model,
                Size = x.Size,
                Price = x.Price,
                ShopId = x.ShopId                
            });
        }

        public BikeDetailsDto GetDetails(long id)
        {
            var bike = _context.Bikes.Include(x => x.Shop).SingleOrDefault(x => x.Id == id);

            if (bike == null)
            {
                return null;
            }
            return new BikeDetailsDto
            {
                Id = bike.Id,
                Brand = bike.Brand,
                Model = bike.Model,
                Size = bike.Size,
                Price = bike.Price,
                Description = bike.Description,
                ShopId = bike.ShopId                
            };
        }

        public void DeleteBike(long id)
        {
            var bike = _context.Bikes.Find(id);
            _context.Remove(bike);
            _context.SaveChanges();
        }
    }
}
