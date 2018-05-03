using RR.Models;
using System;
using System.Collections.Generic;

namespace RR.DomainContracts
{
    public interface IRestaurantService
    {
        List<Restaurant> TopThreeRatedRestaurants();
        List<Restaurant> PartialSearch(string searchTerm);
        List<Restaurant> AllRestaurants(string orderPredicate);
        Restaurant GetById(Guid restaurantId);
        void CreateRestaurant(Restaurant restaurant);
        void UpdateRestaurant(Restaurant restaurant);
        void DeleteRestaurant(Restaurant restaurant);
    }
}
