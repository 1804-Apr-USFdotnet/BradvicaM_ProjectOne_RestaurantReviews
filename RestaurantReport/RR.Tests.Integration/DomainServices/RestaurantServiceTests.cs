using System;
using System.Collections.Generic;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
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
        private readonly RestaurantReportTestContext _testContext;

        public RestaurantServiceTests()
        {
            _testContext = new RestaurantReportTestContext();
            _restaurantService = new RestaurantService(new RestaurantRepository(_testContext));
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

            _testContext.Restaurants.RemoveRange(_testContext.Restaurants);
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

            _testContext.Restaurants.RemoveRange(_testContext.Restaurants);
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

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void PartialSearch_GivenSearchTerm_ReturnsCorrectRestaurants()
        {
            var restuarants = new List<Restaurant>
            {
                new Restaurant{Id = Guid.Parse("5b88c6f9-d35f-4d93-acbe-cc07332024b1"), RestaurantId = Guid.Parse("61cbecb3-2b93-4349-b0b7-4ce6683acc8d"), AverageRating = 9, Name = "Mike", State = "FL", Street = "123 billy St.", ZipCode = 18172, PhoneNumber = "123412312", Website = "www.hithere.com", City = "Tampa"},
                new Restaurant{Id = Guid.Parse("356cc721-6be7-48e2-9c28-e6904a853259"), RestaurantId = Guid.Parse("391b7184-caa9-4f1f-b442-424bae920fa0"), AverageRating = 9, Name = "Jake", State = "FL", Street = "123 Me St.", ZipCode = 18172, PhoneNumber = "123412312", Website = "www.hithere.com", City = "Tampa"},
                new Restaurant{Id = Guid.Parse("a855a758-e250-446d-911d-c603296a77c1"), RestaurantId = Guid.Parse("c369c4b0-a334-4d27-9f79-585c2af86fcb"), AverageRating = 9, Name = "Abe", State = "FL", Street = "123 Me St.", ZipCode = 18172, PhoneNumber = "123412312", Website = "www.hithere.com", City = "Tampa"},
                new Restaurant{Id = Guid.Parse("c11d9615-b4f2-41c4-b23f-030a13389093"), RestaurantId = Guid.Parse("dc8575ca-7282-417b-881a-8e78aa1a9cf6"), AverageRating = 4, Name = "Frank", State = "billy", Street = "123 Me St.", ZipCode = 18172, PhoneNumber = "123412312", Website = "www.hithere.com", City = "Tampa"}
            };

            _testContext.Restaurants.RemoveRange(_testContext.Restaurants);
            _testContext.Restaurants.AddRange(restuarants);
            _testContext.SaveChanges();

            var results = _restaurantService.PartialSearch("billy");

            Approvals.VerifyAll(results, "Restaurants:");
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void Get_GivenOrderPredicate_ReturnsRestaurantsInCorrectOrder()
        {
            var restuarants = new List<Restaurant>
            {
                new Restaurant{Id = Guid.Parse("5b88c6f9-d35f-4d93-acbe-cc07332024b1"), RestaurantId = Guid.Parse("61cbecb3-2b93-4349-b0b7-4ce6683acc8d"), AverageRating = 9, Name = "Mike", State = "FL", Street = "123 billy St.", ZipCode = 18172, PhoneNumber = "123412312", Website = "www.hithere.com", City = "Tampa"},
                new Restaurant{Id = Guid.Parse("356cc721-6be7-48e2-9c28-e6904a853259"), RestaurantId = Guid.Parse("391b7184-caa9-4f1f-b442-424bae920fa0"), AverageRating = 9, Name = "Jake", State = "FL", Street = "123 Me St.", ZipCode = 18172, PhoneNumber = "123412312", Website = "www.hithere.com", City = "Tampa"},
                new Restaurant{Id = Guid.Parse("a855a758-e250-446d-911d-c603296a77c1"), RestaurantId = Guid.Parse("c369c4b0-a334-4d27-9f79-585c2af86fcb"), AverageRating = 9, Name = "Abe", State = "FL", Street = "123 Me St.", ZipCode = 18172, PhoneNumber = "123412312", Website = "www.hithere.com", City = "Tampa"},
                new Restaurant{Id = Guid.Parse("c11d9615-b4f2-41c4-b23f-030a13389093"), RestaurantId = Guid.Parse("dc8575ca-7282-417b-881a-8e78aa1a9cf6"), AverageRating = 4, Name = "Frank", State = "billy", Street = "123 Me St.", ZipCode = 18172, PhoneNumber = "123412312", Website = "www.hithere.com", City = "Tampa"}
            };

            _testContext.Restaurants.RemoveRange(_testContext.Restaurants);
            _testContext.Restaurants.AddRange(restuarants);
            _testContext.SaveChanges();

            var result = _restaurantService.Get("name");

            Approvals.VerifyAll(result, "Restaurants:");
        }

        [TestMethod]
        public void DeleteRestaurant_GivenRestuarnt_DeletesRestuarnt()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void UpdateRestaurant_GivenResturant_UpdatesRestaurant()
        {
            throw new NotImplementedException();
        }
    }
}
