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
    public class ReviewRepositoryTests
    {
        private readonly RestaurantReportTestContext _testContext;
        private readonly ReviewRepository _reviewRepository;

        public ReviewRepositoryTests()
        {
            _testContext = new RestaurantReportTestContext();
            _reviewRepository = new ReviewRepository(_testContext);
        }

        [TestMethod]
        public void GetById_GivenReviewId_ReturnsCorrectReview()
        {
            var review = new Review
            {
                ReviewId = Guid.NewGuid(),
                ReviewPublicId = Guid.NewGuid(),
                Restaurant = _testContext.Restaurants.First()
            };

            _testContext.Reviews.Add(review);
            _testContext.SaveChanges();

            var result = _reviewRepository.GetById(review.ReviewPublicId);

            Assert.AreEqual(review, result);
        }

        [TestMethod]
        public void Get_OnCall_ReturnsAllReviews()
        {
            var reviews = new List<Review>
            {
                new Review
                {
                    ReviewId = Guid.NewGuid(),
                    ReviewPublicId = Guid.NewGuid(),
                    Restaurant = _testContext.Restaurants.First()
                },
                new Review
                {
                    ReviewId = Guid.NewGuid(),
                    ReviewPublicId = Guid.NewGuid(),
                    Restaurant = _testContext.Restaurants.First()
                },
                new Review
                {
                    ReviewId = Guid.NewGuid(),
                    ReviewPublicId = Guid.NewGuid(),
                    Restaurant = _testContext.Restaurants.First()
                },
            };
            
            _testContext.Reviews.RemoveRange(_testContext.Reviews);
            _testContext.Reviews.AddRange(reviews);
            _testContext.SaveChanges();

            var result = _reviewRepository.Get();

            const int expected = 3;

            Assert.AreEqual(expected, result.Count());
        }

        [TestMethod]
        public void Delete_GivenReview_RemovesReview()
        {
            var review = new Review
            {
                ReviewId = Guid.NewGuid(),
                ReviewPublicId = Guid.NewGuid(),
                Restaurant = _testContext.Restaurants.First()
            };

            _testContext.Reviews.Add(review);
            _testContext.SaveChanges();

            _reviewRepository.Delete(review);

            Assert.IsFalse(_reviewRepository.Get().Contains(review));
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void Update_GivenReview_UpdatesReview()
        {
            var beforeUpdate = new Review
            {
                ReviewId = Guid.Parse("4dc863f3-88da-4d3b-958c-ab585a116b9c"),
                ReviewPublicId = Guid.Parse("e5398119-bcc0-4952-a177-2d7aec86fcb5"),
                Restaurant = _testContext.Restaurants.First(),
                ReviewerName = "Mike",
                Comment = "Ok-ish",
                Rating = 5.45
            };

            _testContext.Reviews.RemoveRange(_testContext.Reviews);
            _testContext.Reviews.Add(beforeUpdate);
            _testContext.SaveChanges();

            var afterUpdate = new Review
            {
                ReviewId = beforeUpdate.ReviewId,
                ReviewerName = "John",
                Comment = "It was bad",
                Rating = 1.30,
                ReviewPublicId = beforeUpdate.ReviewPublicId,
                Restaurant = beforeUpdate.Restaurant,
                RestaurantId = beforeUpdate.RestaurantId
            };

            _reviewRepository.Update(afterUpdate);

            var result = _reviewRepository.GetById(beforeUpdate.ReviewPublicId);

            Approvals.Verify(result);
        }

        [TestMethod]
        public void Add_GivenReview_AddsReview()
        {
            var review = new Review
            {
                ReviewId = Guid.NewGuid(),
                ReviewPublicId = Guid.NewGuid(),
                Restaurant = _testContext.Restaurants.First()
            };

            _reviewRepository.Add(review);

            var reviews = _testContext.Reviews.ToList();

            Assert.IsTrue(reviews.Contains(review));
        }
    }
}
