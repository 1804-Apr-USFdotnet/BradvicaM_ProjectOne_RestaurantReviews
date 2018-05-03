using System;
using System.Collections.Generic;
using RR.DomainContracts;
using RR.Models;
using RR.RepositoryContracts;

namespace RR.DomainServices
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public List<Restaurant> TopThreeRatedRestaurants()
        {
            throw new NotImplementedException();
        }

        public List<Restaurant> PartialSearch(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public List<Restaurant> AllRestaurants(string orderPredicate)
        {
            throw new NotImplementedException();
        }

        public Restaurant GetById(Guid restaurantId)
        {
            throw new NotImplementedException();
        }

        public void CreateRestaurant(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }

        public void UpdateRestaurant(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }

        public void DeleteRestaurant(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }
    }
}
