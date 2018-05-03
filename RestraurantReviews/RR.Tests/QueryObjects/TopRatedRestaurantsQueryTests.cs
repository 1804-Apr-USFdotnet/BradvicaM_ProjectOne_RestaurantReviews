using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.Models;
using RR.QueryObjects;

namespace RR.Tests.QueryObjects
{
    [TestClass]
    public class TopRatedRestaurantsQueryTests
    {
        [TestMethod]
        public void AsExpression_Returns_CorrectRestaurants()
        {
            var query = new TopRatedRestaurantsQuery();

            var restaurantList = new List<Restaurant>
            {
                new Restaurant {AverageRating = 4.56},
                new Restaurant {AverageRating = 6.16},
                new Restaurant {AverageRating = 4.56},
                new Restaurant {AverageRating = 1.36},
            };

            var lowReview = new Restaurant{AverageRating = 0.01};

            restaurantList.Add(lowReview);

            var results = query.AsExpression(restaurantList);

            Assert.IsFalse(results.Contains(lowReview));
        }

        [TestMethod]
        public void AsExpression_Returns_CorrectNumberOfRestaurants()
        {
            var query = new TopRatedRestaurantsQuery();

            var restaurantList = new List<Restaurant>
            {
                new Restaurant {AverageRating = 4.56},
                new Restaurant {AverageRating = 6.16},
                new Restaurant {AverageRating = 4.56},
                new Restaurant {AverageRating = 1.36},
            };

            var results = query.AsExpression(restaurantList);

            Assert.IsTrue(results.Count == 3);
        }
    }
}
