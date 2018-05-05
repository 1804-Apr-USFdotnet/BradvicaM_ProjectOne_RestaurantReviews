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
    public class ReviewServiceTests
    {
        private readonly Mock<IReviewRepository> _reviewRepository;
        private readonly ReviewService _reviewService;

        public ReviewServiceTests()
        {
            _reviewRepository = new Mock<IReviewRepository>();
            var restaurantRepository = new Mock<IRestaurantRepository>();
            restaurantRepository.Setup(x => x.Update(It.IsAny<Restaurant>()));
            _reviewRepository.Setup(x => x.Update(It.IsAny<Review>()));
            _reviewRepository.Setup(x => x.Delete(It.IsAny<Review>()));
            _reviewRepository.Setup(x => x.GetById(It.IsAny<Guid>()));

            _reviewService = new ReviewService(_reviewRepository.Object, restaurantRepository.Object);
        }

        [TestMethod]
        public void UpdateReviews_GivenReview_CallsRepositoryMethod()
        {
            _reviewService.UpdateReview(new Review{Restaurant = new Restaurant{Reviews = new List<Review>()}});

            _reviewRepository.Verify(x => x.Update(It.IsAny<Review>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public void DeleteReview_GivenReview_CallsRepositoryMethod()
        {
            _reviewService.DeleteReview(new Review{Restaurant = new Restaurant{Reviews = new List<Review>()}});

            _reviewRepository.Verify(x => x.Delete(It.IsAny<Review>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public void GetById_OnGuid_CallsRepositoryMethod()
        {
            _reviewService.Get(new Guid());

            _reviewRepository.Verify(x => x.GetById(It.IsAny<Guid>()), Times.AtLeastOnce);
        }
    }
}
