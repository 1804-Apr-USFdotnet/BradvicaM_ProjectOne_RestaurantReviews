using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.DomainServices;
using RR.Models;
using RR.Repositories;

namespace RR.Tests.Integration.DomainServices
{
    [TestClass]
    public class RestaurantServiceTests
    {
        [TestMethod]
        public void ReviewRestaurant_GivenNewReview_UpdateRestaurantEntity()
        {
            var restaurantService = new RestaurantService(new RestaurantRepository(new RestaurantReportTestContext()));

            var restaurant = restaurantService.Get().First();

            var review = new Review
            {
                Comment = "junk comment",
                Rating = 5.0,
                ReviewerName = "mike",
                Id = Guid.NewGuid(),
                ReviewId = Guid.NewGuid(),
                Restaurant = restaurant
            };

            restaurantService.ReviewRestaurant(review);

            var sameRestaurant = restaurantService.Get(restaurant.RestaurantId);

            Assert.IsTrue(sameRestaurant.Reviews.Contains(review));
        }

        [TestMethod]
        public void GetById_GivenId_ReturnsCorrectRestaurant()
        {
            var restaurantService = new RestaurantService(new RestaurantRepository(new RestaurantReportTestContext()));

            var restaurant = restaurantService.Get().First();

            var restaurantById = restaurantService.Get(restaurant.RestaurantId);

            Assert.AreEqual(restaurantById, restaurant);
        }
    }
}
