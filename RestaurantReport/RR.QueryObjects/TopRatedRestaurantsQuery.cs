using System.Collections.Generic;
using System.Linq;
using RR.Models;

namespace RR.QueryObjects
{
    public class TopRatedRestaurantsQuery
    {
        private readonly int _numberToTake;
        private readonly IEnumerable<Restaurant> _restaurants;

        public TopRatedRestaurantsQuery(IEnumerable<Restaurant> restaurants, int numberToTake)
        {
            _restaurants = restaurants;
            _numberToTake = numberToTake;
        }

        public List<Restaurant> AsExpression()
        {
            return _restaurants.OrderByDescending(x => x.AverageRating).Take(_numberToTake).ToList();
        }
    }
}
