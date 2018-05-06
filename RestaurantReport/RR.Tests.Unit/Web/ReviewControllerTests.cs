using System;
using System.Web.Mvc;
using Autofac;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RR.DomainContracts;
using RR.Models;
using RR.ViewModels;
using RR.Web;
using RR.Web.Controllers;

namespace RR.Tests.Unit.Web
{
    [TestClass]
    public class ReviewControllerTests
    {
        private readonly ReviewController _controller;

        public ReviewControllerTests()
        {
            var service = new Mock<IReviewService>();
            service.Setup(x => x.Get(It.IsAny<Guid>())).Returns(new Review());
            service.Setup(x => x.UpdateReview(It.IsAny<Review>()));
            service.Setup(x => x.DeleteReview(It.IsAny<Review>()));
            service.Setup(x => x.CreateReview(It.IsAny<Review>(), It.IsAny<Guid>()));

            var container = Bootstrapper.RegisterTypes();
            var mapper = container.Resolve<IMapper>();

            _controller = new ReviewController(service.Object, mapper);
        }

        [TestMethod]
        public void EditReview_OnGetViewModel_ReturnsCorrectView()
        {
            var result = _controller.EditReview(new RestaurantReviewsViewModel()) as ViewResult;

            Assert.AreEqual("EditReview", result.ViewName);
        }

        [TestMethod]
        public void EditReview_OnGetViewModel_ReturnsCorrectViewModel()
        {
            var result = _controller.EditReview(new RestaurantReviewsViewModel()) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(EditReviewViewModel));
        }

        [TestMethod]
        public void EditReview_OnPostViewModelBadModelState_ReturnsCorrectView()
        {
            _controller.ModelState.AddModelError("Error", "Bad");

            var result = _controller.EditReview(new EditReviewViewModel()) as ViewResult;

            Assert.AreEqual("EditReview", result.ViewName);
        }

        [TestMethod]
        public void EditReview_OnPostViewModelBadModelState_ReturnsCorrectViewModel()
        {
            _controller.ModelState.AddModelError("Error", "Bad");

            var result = _controller.EditReview(new EditReviewViewModel()) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(EditReviewViewModel));
        }

        [TestMethod]
        public void EditReview_OnPostViewModel_ReturnsCorrectRedirect()
        {
            var result = _controller.EditReview(new EditReviewViewModel()) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("ListRestaurants", result.RouteValues["Action"]);
            Assert.AreEqual("Restaurant", result.RouteValues["Controller"]);
        }

        [TestMethod]
        public void DeleteReview_OnPostViewModel_ReturnsCorrectRedirect()
        {
            var result = _controller.DeleteReview(new RestaurantReviewsViewModel()) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("ListRestaurants", result.RouteValues["Action"]);
            Assert.AreEqual("Restaurant", result.RouteValues["Controller"]);
        }

        [TestMethod]
        public void CreateReview_OnGetViewModel_ReturnsCorrectView()
        {
            var result = _controller.CreateReview(new ListRestaurantsViewModel()) as ViewResult;

            Assert.AreEqual("CreateReview", result.ViewName);
        }

        [TestMethod]
        public void CreateReview_OnGetViewModel_ReturnsCorrectViewModel()
        {
            var result = _controller.CreateReview(new ListRestaurantsViewModel()) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(CreateReviewViewModel));
        }

        [TestMethod]
        public void CreateReview_OnPostViewModelBadModelState_ReturnsCorrectView()
        {
            _controller.ModelState.AddModelError("Error", "Bad");

            var result = _controller.CreateReview(new CreateReviewViewModel()) as ViewResult;

            Assert.AreEqual("CreateReview", result.ViewName);
        }

        [TestMethod]
        public void CreateReview_OnPostViewModelBadModelState_ReturnsCorrectViewModel()
        {
            _controller.ModelState.AddModelError("Error", "Bad");

            var result = _controller.CreateReview(new CreateReviewViewModel()) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(CreateReviewViewModel));
        }

        [TestMethod]
        public void CreateReview_OnPostViewModel_RedirectsToCorrectAction()
        {
            var result = _controller.CreateReview(new CreateReviewViewModel()) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("ListRestaurants", result.RouteValues["Action"]);
            Assert.AreEqual("Restaurant", result.RouteValues["Controller"]);
        }
    }
}
