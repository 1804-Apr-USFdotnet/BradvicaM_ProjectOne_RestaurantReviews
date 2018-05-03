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
            _reviewService = new ReviewService(new ReviewRepository(_testContext));
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void UpdateReview_GivenReview_UpdatesReview()
        {
            var beforeUpdate = new Review
            {
                ReviewId = Guid.Parse("ec6b66f8-c116-4ed5-ba00-bd0d72edabd7"),
                Id = Guid.Parse("f02ef066-251e-4f90-b404-eb09ab06ec93"),
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
                ReviewId = beforeUpdate.ReviewId,
                Id = beforeUpdate.Id,
                Restaurant = beforeUpdate.Restaurant,
                RestaurantId = beforeUpdate.RestaurantId,
                ReviewerName = "Matt",
                Rating = 7.5,
                Comment = "Goodio"
            };

            _reviewService.UpdateReview(afterUpdate);

            var result = _testContext.Reviews.Find(beforeUpdate.Id);

            Approvals.Verify(result);
        }

        [TestMethod]
        public void DeleteReview_GivenReview_DeleteReview()
        {
            var review = new Review
            {
                ReviewId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                Restaurant = _testContext.Restaurants.First()
            };

            _testContext.Reviews.Add(review);
            _testContext.SaveChanges();

            _reviewService.DeleteReview(review);

            var reviews = _testContext.Reviews.ToList();

            Assert.IsFalse(reviews.Contains(review));
        }
    }
}
