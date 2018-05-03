using System;
using System.Collections.Generic;
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
            _mockRepository = new Mock<IRestaurantRepository>();
            _mockRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Restaurant{Reviews = new List<Review>()});
            _mockRepository.Setup(x => x.Update(It.IsAny<Restaurant>()));

            _restaurantService = new RestaurantService(_mockRepository.Object);
        }

        [TestMethod]
        public void ReviewRestaurant_GivenReview_CallsRepositoryUpdate()
        {
            var review = new Review{Restaurant = new Restaurant()};

            _restaurantService.ReviewRestaurant(review);

            _mockRepository.Verify(x => x.Update(It.IsAny<Restaurant>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public void Get_GivenRestaurantId_CallRepositoryMethod()
        {
            _restaurantService.Get(Guid.NewGuid());

            _mockRepository.Verify(x => x.GetById(It.IsAny<Guid>()), Times.AtLeastOnce);
        }
    }
}
