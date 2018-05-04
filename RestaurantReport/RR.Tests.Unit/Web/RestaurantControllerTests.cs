using System.Collections.Generic;
using System.Web.Mvc;
using Autofac;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RR.DomainContracts;
using RR.Models;
using RR.Web;
using RR.Web.Controllers;

namespace RR.Tests.Unit.Web
{
    [TestClass]
    public class RestaurantControllerTests
    {
        private readonly Mock<IRestaurantService> _restaurantService;
        private readonly IMapper _mapper;

        public RestaurantControllerTests()
        {
            _restaurantService = new Mock<IRestaurantService>();
            _restaurantService.Setup(x => x.Get()).Returns(new List<Restaurant> {new Restaurant()});

            var container = Bootstrapper.RegisterTypes();
            _mapper = container.Resolve<IMapper>();
        }

        [TestMethod]
        public void ListRestaurants_OnCall_ReturnsCorrectView()
        {
            var controller = new RestaurantController(_restaurantService.Object, _mapper);

            var result = controller.ListRestaurants() as ViewResult;

            Assert.AreEqual("ListRestaurants", result.ViewName);
        }
    }
}
