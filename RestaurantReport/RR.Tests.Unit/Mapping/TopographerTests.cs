using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Autofac;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.Mapping;
using RR.Models;
using RR.ViewModels;
using RR.Web;

namespace RR.Tests.Unit.Mapping
{
    [TestClass]
    public class TopographerTests
    {
        private readonly Topographer _topographer;

        public TopographerTests()
        {
            var container = Bootstrapper.RegisterTypes();
            var mapper = container.Resolve<IMapper>();

            _topographer = new Topographer(mapper);
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void Map_GivenEditReviewAndReview_MapsCorrectly()
        {
            var viewModel = new EditReviewViewModel
            {
                Comment = "new comment",
                Rating = 10.0,
                ReviewerName = "new name",
                ReviewPublicId = Guid.Empty
            };
            var review = new Review
            {
                Comment = "old comment",
                Rating = 0.0,
                Restaurant = new Restaurant
                {
                    AverageRating = 4,
                    City = "Old City",
                    Name = "Old Name",
                    PhoneNumber = "1234567890",
                    RestaurantId = Guid.Empty,
                    RestaurantPublicId = Guid.Empty,
                    Reviews = new List<Review>(),
                    State = "Old State",
                    Street = "Old Street",
                    Website = "Old Website"
                },
                RestaurantId = Guid.Empty,
                ReviewerName = "old name",
                ReviewId = Guid.Empty,
                ReviewPublicId = Guid.Empty
            };

            var result = _topographer.Map(viewModel, review);

            Approvals.Verify(result);
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void Map_GivenRestaurantAndRestaurant_MapsCorrectly()
        {
            
        }
    }
}
