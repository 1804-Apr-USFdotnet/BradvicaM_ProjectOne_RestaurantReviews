using System.Collections.Generic;
using System.Linq;
using RR.Models;

namespace RR.QueryObjects
{
    public class RestaurantOrderQuery
    {
        private readonly string _orderBy;
        private readonly IEnumerable<Restaurant> _restaurants;

        public RestaurantOrderQuery(string orderBy, IEnumerable<Restaurant> restaurants)
        {
            _orderBy = orderBy;
            _restaurants = restaurants;
        }

        public List<Restaurant> AsExpression()
        {
            switch (_orderBy.ToLower())
            {
                case "name":
                    return _restaurants.OrderBy(x => x.Name).ToList();
                case "city":
                    return _restaurants.OrderBy(x => x.City).ToList();
                case "state":
                    return _restaurants.OrderBy(x => x.State).ToList();
                case "rating":
                    return _restaurants.OrderByDescending(x => x.AverageRating).ToList();
                default:
                    return _restaurants.OrderBy(x => x.Name).ToList();
            }
        }
    }
}
