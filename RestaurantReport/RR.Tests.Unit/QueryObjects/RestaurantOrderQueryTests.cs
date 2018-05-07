using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.Models;
using RR.QueryObjects;

namespace RR.Tests.Unit.QueryObjects
{
    [TestClass]
    public class FilterRestaurantsQueryTests
    {
        private readonly List<Restaurant> _restaurants;

        public FilterRestaurantsQueryTests()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Name = "Blah",
                    City = "Tampa",
                    State = "FL",
                    AverageRating = 5.00
                },
                new Restaurant
                {
                    Name = "NAA",
                    City = "Kansas City",
                    State = "MO",
                    AverageRating = 10
                }
            };
        }

        [TestMethod]
        public void AsExpression_OnName_ReturnsCorrectOrder()
        {
            var query = new RestaurantOrderQuery("name", _restaurants);

            var result = query.AsExpression();

            Assert.IsTrue(result.First().Name == "Blah");
        }

        [TestMethod]
        public void AsExpression_OnCity_ReturnsCorrectOrder()
        {
            var query = new RestaurantOrderQuery("city", _restaurants);

            var result = query.AsExpression();

            Assert.IsTrue(result.First().City == "Kansas City");
        }

        [TestMethod]
        public void AsExpression_OnState_ReturnsCorrectOrder()
        {
            var query = new RestaurantOrderQuery("state", _restaurants);

            var result = query.AsExpression();

            Assert.IsTrue(result.First().State == "FL");
        }

        [TestMethod]
        public void AsExpression_OnRating_ReturnsCorrectOrder()
        {
            var query = new RestaurantOrderQuery("rating", _restaurants);

            var result = query.AsExpression();

            Assert.IsTrue(result.First().Name == "NAA");
        }

        [TestMethod]
        public void AsExpression_Default_ReturnsCorrectOrder()
        {
            var query = new RestaurantOrderQuery("junk", _restaurants);

            var result = query.AsExpression();

            Assert.IsTrue(result.First().Name == "Blah");
        }
    }
}
