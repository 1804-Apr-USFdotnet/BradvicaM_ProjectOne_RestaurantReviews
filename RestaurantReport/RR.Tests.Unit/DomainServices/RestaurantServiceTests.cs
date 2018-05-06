using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RR.DomainServices;
using RR.Models;
using RR.RepositoryContracts;

namespace RR.Tests.Unit.DomainServices
{
    [TestClass]
    public class RestaurantServiceTests
    {
        private readonly RestaurantService _restaurantService;
        private readonly Mock<IRestaurantRepository> _mockRepository;

        public RestaurantServiceTests()
        {
            var restaurants = new List<Restaurant>
            {
                new Restaurant{AverageRating = 9, Name = "Mike", State = "FL", Street = "123 billy St.", ZipCode = 18172, PhoneNumber = "123412312", Website = "www.hithere.com", City = "Tampa"},
                new Restaurant{AverageRating = 9, Name = "Jake", State = "FL", Street = "123 Me St.", ZipCode = 18172, PhoneNumber = "123412312", Website = "www.hithere.com", City = "Tampa"},
                new Restaurant{AverageRating = 9, Name = "Abe", State = "FL", Street = "123 Me St.", ZipCode = 18172, PhoneNumber = "123412312", Website = "www.hithere.com", City = "Tampa"},
                new Restaurant{AverageRating = 4, Name = "Frank", State = "billy", Street = "123 Me St.", ZipCode = 18172, PhoneNumber = "123412312", Website = "www.hithere.com", City = "Tampa"}
            };

            _mockRepository = new Mock<IRestaurantRepository>();
            _mockRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Restaurant{Reviews = new List<Review>()});
            _mockRepository.Setup(x => x.Update(It.IsAny<Restaurant>()));
            _mockRepository.Setup(x => x.Get()).Returns(restaurants);
            _mockRepository.Setup(x => x.Add(It.IsAny<Restaurant>()));
            _mockRepository.Setup(x => x.Delete(It.IsAny<Restaurant>()));

            _restaurantService = new RestaurantService(_mockRepository.Object);
        }

        [TestMethod]
        public void Get_GivenRestaurantId_CallsRepositoryMethod()
        {
            _restaurantService.Get(Guid.NewGuid());

            _mockRepository.Verify(x => x.GetById(It.IsAny<Guid>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public void Get_NoParameter_CallsRepositoryMethod()
        {
            var result = _restaurantService.Get();

            _mockRepository.Verify(x => x.Get(), Times.AtLeastOnce);
        }

        [TestMethod]
        public void TopThreeRatedRestaurants_OnCall_ReturnsCorrectNumberOfRestaurants()
        {
            var result = _restaurantService.TopThreeRatedRestaurants();

            const int expected = 3;

            Assert.AreEqual(expected, result.Count);
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void TopThreeRatedRestaurants_OnCall_ReturnsCorrectRestaurants()
        {
            var result = _restaurantService.TopThreeRatedRestaurants();

            Approvals.VerifyAll(result, "Restaurants:");
        }

        [TestMethod]
        public void CreateRestaurants_GivenRestaurant_CallsRepositoryMethod()
        {
            var restuarant = new Restaurant();

            _restaurantService.CreateRestaurant(restuarant);

            _mockRepository.Verify(x => x.Add(It.IsAny<Restaurant>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public void UpdateRestaurant_GivenRestaurant_CallsRepositoryMethod()
        {
            var restaurant = new Restaurant();

            _restaurantService.UpdateRestaurant(restaurant);

            _mockRepository.Verify(x => x.Update(It.IsAny<Restaurant>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public void DeleteRestaurant_GivenRestaurant_CallsRepositoryMethod()
        {
            var restaurant = new Restaurant();

            _restaurantService.DeleteRestaurant(restaurant);

            _mockRepository.Verify(x => x.Delete(It.IsAny<Restaurant>()), Times.AtLeastOnce);
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void Get_GivenOrderPedicate_ReturnsRestaurantsInCorrectOrder()
        {
            const string orderPredicate = "name";
            var result = _restaurantService.Get(orderPredicate);

            Approvals.VerifyAll(result, "Restaurants:");
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void PartialSearch_GivenSearchTerm_OnlyReturnsRestaurantsContainingTerm()
        {
            var results = _restaurantService.PartialSearch("billy");

            Approvals.VerifyAll(results, "Restaurants:");
        }
    }
}
