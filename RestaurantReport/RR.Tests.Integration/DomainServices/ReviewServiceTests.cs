using System;
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
    public class ReviewServiceTests
    {
        private readonly ReviewService _reviewService;
        private readonly RestaurantReportTestContext _testContext;

        public ReviewServiceTests()
        {
            _testContext = new RestaurantReportTestContext();
            _reviewService = new ReviewService(new ReviewRepository(_testContext), new RestaurantRepository(_testContext));
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void UpdateReview_GivenReview_UpdatesReview()
        {
            var beforeUpdate = new Review
            {
                ReviewPublicId = Guid.Parse("ec6b66f8-c116-4ed5-ba00-bd0d72edabd7"),
                ReviewId = Guid.Parse("f02ef066-251e-4f90-b404-eb09ab06ec93"),
                Restaurant = _testContext.Restaurants.First(),
                ReviewerName = "John",
                Rating = 5.0,
                Comment = "Bueno"
            };

            _testContext.Reviews.RemoveRange(_testContext.Reviews);
            _testContext.Reviews.Add(beforeUpdate);
            _testContext.SaveChanges();

            var afterUpdate = new Review
            {
                ReviewPublicId = beforeUpdate.ReviewPublicId,
                ReviewId = beforeUpdate.ReviewId,
                Restaurant = beforeUpdate.Restaurant,
                RestaurantId = beforeUpdate.RestaurantId,
                ReviewerName = "Matt",
                Rating = 7.5,
                Comment = "Goodio"
            };

            _reviewService.UpdateReview(afterUpdate);

            var result = _testContext.Reviews.Find(beforeUpdate.ReviewId);

            Approvals.Verify(result);
        }

        [TestMethod]
        public void DeleteReview_GivenReview_DeleteReview()
        {
            var review = new Review
            {
                ReviewPublicId = Guid.NewGuid(),
                ReviewId = Guid.NewGuid(),
                Restaurant = _testContext.Restaurants.First()
            };

            _testContext.Reviews.Add(review);
            _testContext.SaveChanges();

            _reviewService.DeleteReview(review);

            var reviews = _testContext.Reviews.ToList();

            Assert.IsFalse(reviews.Contains(review));
        }

        [TestMethod]
        public void Get_GivenGuid_ReturnsCorrectReview()
        {
            var review = new Review
            {
                ReviewPublicId = Guid.NewGuid(),
                ReviewId = Guid.NewGuid(),
                Restaurant = _testContext.Restaurants.First()
            };

            _testContext.Reviews.Add(review);
            _testContext.SaveChanges();

            var result = _reviewService.Get(review.ReviewPublicId);

            Assert.AreEqual(review, result);
        }
    }
}
