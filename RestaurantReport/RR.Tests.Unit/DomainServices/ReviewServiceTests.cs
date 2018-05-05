using System;
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
        private readonly Mock<IReviewRepository> _mockRepository;
        private readonly ReviewService _reviewRepository;

        public ReviewServiceTests()
        {
            _mockRepository = new Mock<IReviewRepository>();
            _mockRepository.Setup(x => x.Update(It.IsAny<Review>()));
            _mockRepository.Setup(x => x.Delete(It.IsAny<Review>()));
            _mockRepository.Setup(x => x.GetById(It.IsAny<Guid>()));

            _reviewRepository = new ReviewService(_mockRepository.Object);
        }

        [TestMethod]
        public void UpdateReviews_GivenReview_CallsRepositoryMethod()
        {
            _reviewRepository.UpdateReview(new Review());

            _mockRepository.Verify(x => x.Update(It.IsAny<Review>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public void DeleteReview_GivenReview_CallsRepositoryMethod()
        {
            _reviewRepository.DeleteReview(new Review());

            _mockRepository.Verify(x => x.Delete(It.IsAny<Review>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public void GetById_OnGuid_CallsRepositoryMethod()
        {
            _reviewRepository.Get(new Guid());

            _mockRepository.Verify(x => x.GetById(It.IsAny<Guid>()), Times.AtLeastOnce);
        }
    }
}
