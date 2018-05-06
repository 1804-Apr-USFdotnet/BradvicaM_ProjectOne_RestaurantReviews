using System;
using System.Collections.Generic;
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
    public class RestaurantControllerTests
    {
        private readonly RestaurantController _controller;

        public RestaurantControllerTests()
        {
            var restaurantService = new Mock<IRestaurantService>();
            restaurantService.Setup(x => x.Get()).Returns(new List<Restaurant> {new Restaurant()});
            restaurantService.Setup(x => x.Get(It.IsAny<string>())).Returns(new List<Restaurant>());
            restaurantService.Setup(x => x.Get(It.IsAny<Guid>())).Returns(new Restaurant{Reviews = new List<Review>()});
            restaurantService.Setup(x => x.TopThreeRatedRestaurants()).Returns(new List<Restaurant> {new Restaurant()});
            restaurantService.Setup(x => x.PartialSearch(It.IsAny<string>())).Returns(new List<Restaurant> {new Restaurant()});
            restaurantService.Setup(x => x.CreateRestaurant(It.IsAny<Restaurant>()));
            restaurantService.Setup(x => x.UpdateRestaurant(It.IsAny<Restaurant>()));
            restaurantService.Setup(x => x.DeleteRestaurant(It.IsAny<Restaurant>()));

            var container = Bootstrapper.RegisterTypes();
            var mapper = container.Resolve<IMapper>();
            _controller = new RestaurantController(restaurantService.Object, mapper);
        }

        [TestMethod]
        public void Index_OnCall_ReturnsCorrectView()
        {
            var result = _controller.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void ListRestaurants_NoParameter_ReturnsCorrectView()
        {
            var result = _controller.ListRestaurants() as ViewResult;

            Assert.AreEqual("ListRestaurants", result.ViewName);
        }

        [TestMethod]
        public void ListRestaurants_NoParameter_ReturnsCorrectViewModel()
        {
            var result = _controller.ListRestaurants() as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(ListRestaurantsViewModel));
        }

        [TestMethod]
        public void ListRestaurants_OnPostViewModel_ReturnsCorrectView()
        {
            var result = _controller.ListRestaurants(new ListRestaurantsViewModel()) as ViewResult;

            Assert.AreEqual("ListRestaurants", result.ViewName);
        }

        [TestMethod]
        public void ListRestaurants_OnPostViewModel_ReturnsCorrectViewModel()
        {
            var result = _controller.ListRestaurants(new ListRestaurantsViewModel()) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(ListRestaurantsViewModel));
        }

        [TestMethod]
        public void TopRatedRestaurants_OnCall_ReturnsCorrectView()
        {
            var result = _controller.TopRatedRestaurants() as ViewResult;

            Assert.AreEqual("TopRatedRestaurants", result.ViewName);
        }

        [TestMethod]
        public void TopRatedRestaurants_OnCall_ReturnsCorrectViewModel()
        {
            var result = _controller.TopRatedRestaurants() as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<TopRatedRestaurantViewModel>));
        }

        [TestMethod]
        public void RestaurantDetails_OnPostGuid_ReturnsCorrectView()
        {
            var result = _controller.RestaurantDetails(Guid.NewGuid()) as ViewResult;

            Assert.AreEqual("RestaurantDetails", result.ViewName);
        }

        [TestMethod]
        public void RestaurantDetails_OnPostGuid_ReturnsCorretViewModel()
        {
            var result = _controller.RestaurantDetails(Guid.NewGuid()) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(ViewRestaurantViewModel));
        }

        [TestMethod]
        public void RestaurantSearch_OnPostString_ReturnsCorrectView()
        {
            var result = _controller.RestaurantSearch(It.IsAny<string>()) as ViewResult;

            Assert.AreEqual("ListRestaurants", result.ViewName);
        }

        [TestMethod]
        public void RestaurantSearch_OnPostString_ReturnsCorrectViewModel()
        {
            var result = _controller.RestaurantSearch(It.IsAny<string>()) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(ListRestaurantsViewModel));
        }

        [TestMethod]
        public void ViewReviews_OnPostGuid_ReturnsCorrectView()
        {
            var result = _controller.ViewReviews(new ListRestaurantsViewModel()) as ViewResult;

            Assert.AreEqual("ViewReviews", result.ViewName);
        }

        [TestMethod]
        public void ViewReviews_OnPostGuid_ReturnsCorrectViewModel()
        {
            var result = _controller.ViewReviews(new ListRestaurantsViewModel()) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(RestaurantReviewsViewModel));
        }

        [TestMethod]
        public void CreateRestaurant_OnCall_ReturnsCorrectView()
        {
            var result = _controller.CreateRestaurant() as ViewResult;

            Assert.AreEqual("CreateRestaurant", result.ViewName);
        }

        [TestMethod]
        public void CreateRestaurant_OnPostViewModel_ReturnsCorrectAction()
        {
            var result = _controller.CreateRestaurant(new CreateRestaurantViewModel()) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("ListRestaurants", result.RouteValues["Action"]);
        }

        [TestMethod]
        public void CreateRestaurant_OnInvalidModelState_ReturnsCorrectView()
        {
            _controller.ModelState.AddModelError("Bad", "Error");

            var result = _controller.CreateRestaurant(new CreateRestaurantViewModel()) as ViewResult;

            Assert.AreEqual("CreateRestaurant", result.ViewName);
        }

        [TestMethod]
        public void CreateRestaurant_OnInvalidModelState_ReturnsCorrectViewModel()
        {
            _controller.ModelState.AddModelError("Bad", "Error");

            var result = _controller.CreateRestaurant(new CreateRestaurantViewModel()) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(CreateRestaurantViewModel));
        }

        //No Tests For Create Review, Gonna Move

        [TestMethod]
        public void EditRestaurant_OnGetViewModel_ReturnsCorrectView()
        {
            var result = _controller.EditRestaurant(new ListRestaurantsViewModel()) as ViewResult;

            Assert.AreEqual("EditRestaurant", result.ViewName);
        }

        [TestMethod]
        public void EditRestaurant_OnGetViewModel_ReturnsCorrectViewModel()
        {
            var result = _controller.EditRestaurant(new ListRestaurantsViewModel()) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(EditRestaurantViewModel));
        }

        [TestMethod]
        public void EditRestaurant_OnPostViewModelBadModelState_ReturnsCorrectView()
        {
            _controller.ModelState.AddModelError("Bad", "Error");

            var result = _controller.EditRestaurant(new EditRestaurantViewModel()) as ViewResult;

            Assert.AreEqual(result.ViewName, "EditRestaurant");
        }

        [TestMethod]
        public void EditRestaurant_OnPostViewModelBadModelState_ReturnsCorrectViewModel()
        {
            _controller.ModelState.AddModelError("Bad", "Error");

            var result = _controller.EditRestaurant(new EditRestaurantViewModel()) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(EditRestaurantViewModel));
        }

        [TestMethod]
        public void EditRestaurant_OnPostViewModel_RedirectsToCorrectAction()
        {
            var result = _controller.EditRestaurant(new EditRestaurantViewModel()) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("ListRestaurants", result.RouteValues["Action"]);
        }

        [TestMethod]
        public void DeleteRestaurant_OnPostViewModel_RedirectsToCorrectAction()
        {
            var result = _controller.DeleteRestaurant(new ListRestaurantsViewModel()) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("ListRestaurants", result.RouteValues["Action"]);
        }
    }
}
