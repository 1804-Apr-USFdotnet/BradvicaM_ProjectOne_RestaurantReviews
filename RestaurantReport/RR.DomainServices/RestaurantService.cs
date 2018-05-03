using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Restaurant> Get(string orderPredicate)
        {
            throw new NotImplementedException();
        }

        public List<Restaurant> Get()
        {
            return _restaurantRepository.Get().ToList();
        }

        public void ReviewRestaurant(Review review)
        {
            var restaurant = _restaurantRepository.GetById(review.Restaurant.RestaurantId);

            restaurant.Reviews.Add(review);
            restaurant.CalculateAverageRating(restaurant.Reviews);

            _restaurantRepository.Update(restaurant);
        }

        public Restaurant Get(Guid restaurantId)
        {
            return _restaurantRepository.GetById(restaurantId);
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
