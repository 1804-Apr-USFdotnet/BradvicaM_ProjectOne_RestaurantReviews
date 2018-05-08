using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ApprovalTests;
using ApprovalTests.Reporters;
using Autofac;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.DomainServices;
using RR.Models;
using RR.Repositories;
using RR.ViewModels;
using RR.Web;
using RR.Web.Controllers;

namespace RR.Tests.Integration.Controllers
{
    [TestClass]
    public class ReviewControllerTests
    {
        private readonly RestaurantReportTestContext _testContext;
        private readonly ReviewController _controller;

        public ReviewControllerTests()
        {
            _testContext = new RestaurantReportTestContext();
            var container = Bootstrapper.RegisterTypes();
            var mapper = container.Resolve<IMapper>();

            _controller = new ReviewController(new ReviewService(new ReviewRepository(_testContext), new RestaurantRepository(_testContext)), mapper);
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void EditReview_OnGetGivenViewModel_ReturnsCorrectInformation()
        {
            var review = new Review
            {
                Restaurant = _testContext.Restaurants.First(),
                ReviewId = Guid.Parse("9e648277-e81f-44a8-9b32-519c3d1c6afc"),
                ReviewPublicId = Guid.Parse("649ec18a-910e-4ee8-a805-d0c0343b7853"),
                ReviewerName = "Default",
                Comment = "Default Comment",
                Rating = 1
            };
            _testContext.Reviews.RemoveRange(_testContext.Reviews);
            _testContext.Reviews.Add(review);
            _testContext.SaveChanges();

            var result = _controller.EditReview(new RestaurantReviewsViewModel{SelectReviewPublicId = review.ReviewPublicId}) as ViewResult;

            Approvals.Verify(result.Model.ToString());
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void EditReview_OnPostViewModel_UpdatesReview()
        {
            var builder = new StringBuilder();
            var review = new Review
            {
                Restaurant = _testContext.Restaurants.First(),
                ReviewId = Guid.Parse("6daba6ac-f46f-4d22-8715-875829dff646"),
                ReviewPublicId = Guid.Parse("f9ebf30b-d677-4bed-9f82-eed80b52d431"),
                ReviewerName = "Default",
                Comment = "Default Comment",
                Rating = 1
            };
            builder.Append(review);

            _testContext.Reviews.RemoveRange(_testContext.Reviews);
            _testContext.Reviews.Add(review);
            _testContext.SaveChanges();

            _controller.EditReview(new EditReviewViewModel
            {
                Comment = "New Comment",
                Rating = 10.0,
                ReviewerName = "New Name",
                ReviewPublicId = review.ReviewPublicId
            });

            var result = _testContext.Reviews.Find(review.ReviewId);
            
            builder.Append(result);

            Approvals.Verify(builder.ToString());
        }

        [TestMethod]
        public void DeleteReview_OnPostViewModel_DeletesReview()
        {
            var review = new Review
            {
                Restaurant = _testContext.Restaurants.First(),
                ReviewId = Guid.Parse("80d38bb3-026e-4911-917e-f3e9f0e3a2c9"),
                ReviewPublicId = Guid.Parse("3176bb5f-c69a-48fe-900d-10d3e3a6f58e"),
                ReviewerName = "Default",
                Comment = "Default Comment",
                Rating = 1
            };
            _testContext.Reviews.RemoveRange(_testContext.Reviews);
            _testContext.Reviews.Add(review);
            _testContext.SaveChanges();

            _controller.DeleteReview(new RestaurantReviewsViewModel {SelectReviewPublicId = review.ReviewPublicId});

            var reviews = _testContext.Reviews.ToList();

            Assert.IsFalse(reviews.Contains(review));
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void CreateReview_OnGetViewModel_ReturnsCorrectInformation()
        {
            var viewModel = new ListRestaurantsViewModel
            {
                ListOrder = "List Order",
                SelectRestaurantPublicId = Guid.Parse("3c1c1b2d-90c5-4be1-a433-65e19dc50896"),
                ViewRestaurantViewModels = new List<ViewRestaurantViewModel>()
            };

            var result = _controller.CreateReview(viewModel) as ViewResult;

            Approvals.Verify(result.Model.ToString());
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void CreateReview_OnPostViewModel_CreatesReview()
        {
            var viewModel = new CreateReviewViewModel
            {
                Comment = "My Comment",
                Rating = 5.0,
                ReviewerName = "My Name",
                RestaurantPublicId = _testContext.Restaurants.First().RestaurantPublicId
            };

            _controller.CreateReview(viewModel);

            var reviews = _testContext.Reviews.ToList();

            Assert.IsTrue(reviews.Exists(x => x.Restaurant.RestaurantPublicId == viewModel.RestaurantPublicId));
        }
    }
}
