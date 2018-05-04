using System;
using System.Collections.Generic;
using System.Linq;
using RR.DomainContracts;
using RR.Models;
using RR.QueryObjects;
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
            var allRestaurants = _restaurantRepository.Get();

            const int takeTopThree = 3;
            var query = new TopRatedRestaurantsQuery(allRestaurants, takeTopThree);

            return query.AsExpression();
        }

        public List<Restaurant> PartialSearch(string searchTerm)
        {
            var allRestaurants = _restaurantRepository.Get();

            var query = new PartialSearchQuery(searchTerm, allRestaurants);

            return query.AsExpression();
        }

        public List<Restaurant> Get(string orderPredicate)
        {
            var allRestaurants = _restaurantRepository.Get();

            var query = new RestaurantOrderQuery(orderPredicate, allRestaurants);

            return query.AsExpression();
        }

        public List<Restaurant> Get()
        {
            return _restaurantRepository.Get().ToList();
        }

        public void ReviewRestaurant(Review review)
        {
            var restaurant = _restaurantRepository.GetById(review.Restaurant.RestaurantPublicId);

            review.Restaurant = null;

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
            _restaurantRepository.Add(restaurant);
        }

        public void UpdateRestaurant(Restaurant restaurant)
        {
            _restaurantRepository.Update(restaurant);
        }

        public void DeleteRestaurant(Restaurant restaurant)
        {
            _restaurantRepository.Delete(restaurant);
        }
    }
}
