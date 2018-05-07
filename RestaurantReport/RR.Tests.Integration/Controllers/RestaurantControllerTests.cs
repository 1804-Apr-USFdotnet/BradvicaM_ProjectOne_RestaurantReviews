using System.Collections.Generic;
using System.Web.Mvc;
using ApprovalTests;
using ApprovalTests.Reporters;
using Autofac;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.DomainServices;
using RR.Repositories;
using RR.ViewModels;
using RR.Web;
using RR.Web.Controllers;

namespace RR.Tests.Integration.Controllers
{
    [TestClass]
    public class RestaurantControllerTests
    {
        private readonly RestaurantReportTestContext _testContext;
        private readonly RestaurantController _controller;

        public RestaurantControllerTests()
        {
            var container = Bootstrapper.RegisterTypes();
            var mapper = container.Resolve<IMapper>();
            _testContext = new RestaurantReportTestContext();

            _controller = new RestaurantController(new RestaurantService(new RestaurantRepository(_testContext)), mapper);
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void ListRestaurants_OnGet_ReturnsCorrectInformation()
        {
            var result = _controller.ListRestaurants() as ViewResult;

            Approvals.Verify(result.Model.ToString());
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void ListRestaurants_OnPostViewModel_ReturnsCorrectInformation()
        {
            var result = _controller.ListRestaurants(new ListRestaurantsViewModel {ListOrder = "name"}) as ViewResult;

            Approvals.Verify(result.Model.ToString());
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void TopRatedRestauarants_OnCall_ReturnsCorrectInformation()
        {
            var result = _controller.TopRatedRestaurants() as ViewResult;

            Approvals.VerifyAll(result.Model as IEnumerable<TopRatedRestaurantViewModel>, "TopRatedRestaurants:");
        }
    }
}
