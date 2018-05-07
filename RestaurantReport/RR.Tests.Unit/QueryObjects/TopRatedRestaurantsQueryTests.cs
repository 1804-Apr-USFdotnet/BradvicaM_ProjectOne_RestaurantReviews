using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.Models;
using RR.QueryObjects;

namespace RR.Tests.Unit.QueryObjects
{
    [TestClass]
    public class TopRatedRestaurantsQueryTests
    {
        [TestMethod]
        public void AsExpression_Returns_CorrectRestaurants()
        {
            var lowReview = new Restaurant { AverageRating = 0.01 };
            var restaurantList = new List<Restaurant>
            {
                new Restaurant {AverageRating = 4.56},
                new Restaurant {AverageRating = 6.16},
                new Restaurant {AverageRating = 4.56},
                lowReview
            };

            const int numberToTake = 3;
            var query = new TopRatedRestaurantsQuery(restaurantList, numberToTake);

            var results = query.AsExpression();

            Assert.IsFalse(results.Contains(lowReview));
        }

        [TestMethod]
        public void AsExpression_Returns_CorrectNumberOfRestaurants()
        {
            var restaurantList = new List<Restaurant>
            {
                new Restaurant {AverageRating = 4.56},
                new Restaurant {AverageRating = 6.16},
                new Restaurant {AverageRating = 4.56},
                new Restaurant {AverageRating = 1.36},
            };

            const int numberToTake = 3;
            var query = new TopRatedRestaurantsQuery(restaurantList, numberToTake);

            var results = query.AsExpression();

            Assert.AreEqual(numberToTake, results.Count);
        }
    }
}
