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
        private readonly RestaurantService _restaurantService;
        private readonly RestaurantReportTestContext _textContext;

        public RestaurantServiceTests()
        {
            _textContext = new RestaurantReportTestContext();
            _restaurantService = new RestaurantService(new RestaurantRepository(_textContext));
        }

        [TestMethod]
        public void ReviewRestaurant_GivenNewReview_UpdateRestaurantEntity()
        {
            var restaurant = _restaurantService.Get().First();

            var review = new Review
            {
                Comment = "junk comment",
                Rating = 5.0,
                ReviewerName = "mike",
                Id = Guid.NewGuid(),
                ReviewId = Guid.NewGuid(),
                Restaurant = restaurant
            };

            _restaurantService.ReviewRestaurant(review);

            var sameRestaurant = _restaurantService.Get(restaurant.RestaurantId);

            Assert.IsTrue(sameRestaurant.Reviews.Contains(review));
        }

        [TestMethod]
        public void GetById_GivenId_ReturnsCorrectRestaurant()
        {
            var restaurant = _restaurantService.Get().First();

            var restaurantById = _restaurantService.Get(restaurant.RestaurantId);

            Assert.AreEqual(restaurantById, restaurant);
        }

        [TestMethod]
        public void Get_NoParameters_ReturnsAllRestaurants()
        {
            var first = new Restaurant { Id = Guid.NewGuid(), RestaurantId = Guid.NewGuid(), AverageRating = 10 };
            var second = new Restaurant { Id = Guid.NewGuid(), RestaurantId = Guid.NewGuid(), AverageRating = 10 };
            var third = new Restaurant { Id = Guid.NewGuid(), RestaurantId = Guid.NewGuid(), AverageRating = 10 };

            _textContext.Restaurants.RemoveRange(_textContext.Restaurants);
            _restaurantService.CreateRestaurant(first);
            _restaurantService.CreateRestaurant(second);
            _restaurantService.CreateRestaurant(third);

            var result = _restaurantService.Get();

            const int expected = 3;

            Assert.AreEqual(expected, result.Count);
        }

        [TestMethod]
        public void TopThreeRatedRestaurants_OnCall_ReturnsCorrectRestaurants()
        {
            var first = new Restaurant { Id = Guid.NewGuid(), RestaurantId = Guid.NewGuid(), AverageRating = 10};
            var second = new Restaurant { Id = Guid.NewGuid(), RestaurantId = Guid.NewGuid(), AverageRating = 10 };
            var third = new Restaurant { Id = Guid.NewGuid(), RestaurantId = Guid.NewGuid(), AverageRating = 10 };
            var fourth = new Restaurant { Id = Guid.NewGuid(), RestaurantId = Guid.NewGuid(), AverageRating = 9 };

            _textContext.Restaurants.RemoveRange(_textContext.Restaurants);
            _restaurantService.CreateRestaurant(first);
            _restaurantService.CreateRestaurant(second);
            _restaurantService.CreateRestaurant(third);
            _restaurantService.CreateRestaurant(fourth);

            var allRestaurants = _restaurantService.TopThreeRatedRestaurants();

            Assert.IsTrue(allRestaurants.Contains(first));
            Assert.IsTrue(allRestaurants.Contains(second));
            Assert.IsTrue(allRestaurants.Contains(third));
        }

        [TestMethod]
        public void CreateRestaurants_GivenRestaurant_AddsRestaurants()
        {
            var restaurant = new Restaurant{Id = Guid.NewGuid(), RestaurantId = Guid.NewGuid()};

            _restaurantService.CreateRestaurant(restaurant);

            var allRestaurants = _restaurantService.Get();

            Assert.IsTrue(allRestaurants.Contains(restaurant));
        }
    }
}
