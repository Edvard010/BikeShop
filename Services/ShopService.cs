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
    public class ShopService
    {
        private readonly BikeShopContext _context;
        private readonly IConfiguration _config;
        public ShopService(BikeShopContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        public ShopDetailsDto GetDetails(long id)
        {
            var shop = _context.Shops.Include(x => x.Bikes).SingleOrDefault(x => x.Id == id);

            if (shop == null)
            {
                return null;
            }
            return new ShopDetailsDto
            {
                Id = shop.Id,
                Name = shop.Name,
                Address = shop.Address,
                Email = shop.Email,
                Phone = shop.Phone,
                Description = shop.Description,
                Bikes = shop.Bikes.Select(p => new BikeItemDto
                {
                    Id = p.Id,
                    Brand = p.Brand,
                    Model = p.Model,
                    Size = p.Size,
                    Description = p.Description,
                    Price = p.Price
                })
            };
        }
        public bool Create(NewShopDto shop)
        {
            if (!_context.Shops.Any(x => x.Address == shop.Address))
            {
                var newShop = new Shop
                {
                    Name = shop.Name,
                    Address = shop.Address,
                    Phone = shop.Phone,
                    Email = shop.Email,
                    Description = shop.Description
                };
                _context.Shops.Add(newShop);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<ShopItemDto> GetAll()
        {
            return _context.Shops.Include(x => x.Bikes).ToList().Select(x => new ShopItemDto
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                Phone = x.Phone,
                Description = x.Description,
                Email = x.Email,
                Bikes = x.Bikes.Count() //presents how many bikes, but maybe it will be better to present list of bikes(brand,name,size)
            });
        }

        public bool EditShop(long id, ShopDetailsDto shopChanges)
        {
            var shop = _context.Shops.SingleOrDefault(x => x.Id == id);
            if (shop != null)
            {
                {
                    shop.Name = shopChanges.Name;
                    shop.Address = shopChanges.Address;
                    shop.Description = shopChanges.Description;
                    shop.Email = shopChanges.Email;
                    shop.Phone = shopChanges.Phone;
                }
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
