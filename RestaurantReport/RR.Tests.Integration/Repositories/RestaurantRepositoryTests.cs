using System;
using System.Collections.Generic;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.Models;
using RR.Repositories;

namespace RR.Tests.Integration.Repositories
{
    [TestClass]
    public class RestaurantRepositoryTests
    {
        private readonly RestaurantReportTestContext _textContext;
        private readonly RestaurantRepository _restaurantRepository;

        public RestaurantRepositoryTests()
        {
            _textContext = new RestaurantReportTestContext();
            _restaurantRepository = new RestaurantRepository(_textContext);
        }

        [TestMethod]
        public void GetById_GivenRestaurantId_ReturnsCorrectRestuarant()
        {
            var restaurant = new Restaurant
            {
                Id = Guid.NewGuid(),
                RestaurantId = Guid.NewGuid()
            };

            _textContext.Restaurants.Add(restaurant);
            _textContext.SaveChanges();

            var result = _restaurantRepository.GetById(restaurant.RestaurantId);

            Assert.AreEqual(restaurant, result);
        }

        [TestMethod]
        public void Get_OnCall_ReturnsAllRestuarants()
        {
            var restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Id = Guid.NewGuid(),
                    RestaurantId = Guid.NewGuid()
                },
                new Restaurant
                {
                    Id = Guid.NewGuid(),
                    RestaurantId = Guid.NewGuid()
                },
                new Restaurant
                {
                    Id = Guid.NewGuid(),
                    RestaurantId = Guid.NewGuid()
                }
            };

            _textContext.Restaurants.RemoveRange(_textContext.Restaurants);
            _textContext.Restaurants.AddRange(restaurants);
            _textContext.SaveChanges();

            var result = _restaurantRepository.Get();

            const int expected = 3;

            Assert.AreEqual(expected, result.Count());
        }

        [TestMethod]
        public void Delete_GivenRestaurant_RemovesRestaurant()
        {
            var restaurant = new Restaurant
            {
                Id = Guid.NewGuid(),
                RestaurantId = Guid.NewGuid()
            };

            _textContext.Restaurants.Add(restaurant);
            _textContext.SaveChanges();

            _restaurantRepository.Delete(restaurant);

            Assert.IsFalse(_restaurantRepository.Get().Contains(restaurant));
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void Update_GivenRestaurant_UpdatesRestaurant()
        {
            var beforeUpdate = new Restaurant
            {
                Id = Guid.Parse("3d18e4bf-768d-44ec-b3a8-02e8eec06d56"),
                RestaurantId = Guid.Parse("5b2805a3-eadc-4fa0-9eba-1caab4620e07"),
                AverageRating = 5.43,
                City = "Tampa",
                Name = "Jakes Del Tampa",
                ZipCode = 81723,
                PhoneNumber = "1827189291",
                State = "FL",
                Street = "123 Me St.",
                Website = "www.jakes.com"
            };

            _textContext.Restaurants.RemoveRange(_textContext.Restaurants);
            _textContext.Restaurants.Add(beforeUpdate);
            _textContext.SaveChanges();

            var afterUpdate = new Restaurant
            {
                Id = beforeUpdate.Id,
                RestaurantId = beforeUpdate.RestaurantId,
                AverageRating = 1.23,
                City = "Del Mar",
                Name = "Mikes Tampa",
                ZipCode = 91832,
                PhoneNumber = "9876253412",
                State = "CA",
                Street = "45 Genevieve Dr.",
                Website = "www.mikes.com"
            };

            _restaurantRepository.Update(afterUpdate);

            var result = _restaurantRepository.GetById(beforeUpdate.RestaurantId);

            Approvals.Verify(result);
        }

        [TestMethod]
        public void Add_GivenRestaurant_AddsRestaurant()
        {
            var restaurant = new Restaurant
            {
                Id = Guid.NewGuid(),
                RestaurantId = Guid.NewGuid()
            };

            _restaurantRepository.Add(restaurant);

            var result = _restaurantRepository.Get();

            Assert.IsTrue(result.Contains(restaurant));
        }
    }
}
