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
        public void TopRatedRestuarants_OnPostViewModel_ReturnsCorrectViewModel()
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
    }
}
