using System;
using System.Collections.Generic;
using RR.Models;

namespace RR.RepositoryContracts
{
    public interface IRestaurantRepository
    {
        Restaurant GetById(Guid restaurantId);
        IEnumerable<Restaurant> Get();
        void Add(Restaurant restaurant);
        void Update(Restaurant restaurant);
        void Delete(Restaurant restaurant);
    }
}
