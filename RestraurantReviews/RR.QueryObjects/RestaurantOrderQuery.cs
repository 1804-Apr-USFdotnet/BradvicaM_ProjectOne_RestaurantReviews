using System.Collections.Generic;
using System.Linq;
using RR.Models;

namespace RR.QueryObjects
{
    public class RestaurantOrderQuery
    {
        private readonly string _orderBy;

        public RestaurantOrderQuery(string orderBy)
        {
            _orderBy = orderBy;
        }

        public List<Restaurant> AsExpression(IEnumerable<Restaurant> restaurants)
        {
            switch (_orderBy.ToLower())
            {
                case "name":
                    return restaurants.OrderBy(x => x.Name).ToList();
                case "city":
                    return restaurants.OrderBy(x => x.City).ToList();
                case "state":
                    return restaurants.OrderBy(x => x.State).ToList();
                case "rating":
                    return restaurants.OrderByDescending(x => x.AverageRating).ToList();
                default:
                    return restaurants.OrderBy(x => x.Name).ToList();
            }
        }
    }
}
