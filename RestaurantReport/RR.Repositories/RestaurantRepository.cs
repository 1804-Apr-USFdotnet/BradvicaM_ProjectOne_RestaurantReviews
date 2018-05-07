using System;
using System.Collections.Generic;
using System.Linq;
using RR.Models;
using RR.RepositoryContracts;

namespace RR.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly IContext _context;

        public RestaurantRepository(IContext context)
        {
            _context = context;
        }

        public Restaurant GetById(Guid restaurantId)
        {
            return _context.Restaurants.First(x => x.RestaurantPublicId == restaurantId);
        }

        public IEnumerable<Restaurant> Get()
        {
            return _context.Restaurants;
        }

        public void Add(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
        }

        public void Update(Restaurant restaurant)
        {
            var entity = _context.Restaurants.Find(restaurant.RestaurantId);
            _context.Entry(entity).CurrentValues.SetValues(restaurant);
            _context.SaveChanges();
        }

        public void Delete(Restaurant restaurant)
        {
            _context.Restaurants.Remove(restaurant);
            _context.SaveChanges();
        }
    }
}
