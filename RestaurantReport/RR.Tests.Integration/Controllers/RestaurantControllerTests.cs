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
            var restaurant = new Restaurant
            {
                RestaurantId = Guid.Parse("2c43d66e-260b-409e-981d-8a588a55c8de"),
                RestaurantPublicId = Guid.Parse("d2a9326c-7a95-4faa-b324-c5349839f447"),
                AverageRating = 1.0,
                City = "Jake",
                Name = "Some Name",
                PhoneNumber = "1234567890",
                Reviews = new List<Review>(),
                State = "SA",
                Street = "123 Some St.",
                Website = "www.whatever.com"
            };

            _testContext.Restaurants.RemoveRange(_testContext.Restaurants);
            _testContext.Restaurants.Add(restaurant);
            _testContext.SaveChanges();

            var result = _controller.ListRestaurants() as ViewResult;

            Approvals.Verify(result.Model.ToString());
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void ListRestaurants_OnPostViewModel_ReturnsCorrectInformation()
        {
            var restaurant = new Restaurant
            {
                RestaurantId = Guid.Parse("fa4c6ca8-c81d-4be2-9796-e1fd4fc0ff5c"),
                RestaurantPublicId = Guid.Parse("6897a4b8-179d-4ece-b7f9-f695fba8aba1"),
                AverageRating = 1.0,
                City = "Jake",
                Name = "Some Name",
                PhoneNumber = "1234567890",
                Reviews = new List<Review>(),
                State = "SA",
                Street = "123 Some St.",
                Website = "www.whatever.com"
            };

            _testContext.Restaurants.RemoveRange(_testContext.Restaurants);
            _testContext.Restaurants.Add(restaurant);
            _testContext.SaveChanges();

            var result = _controller.OrderListRestaurants(new ListRestaurantsViewModel {ListOrder = "name"}) as ViewResult;

            Approvals.Verify(result.Model.ToString());
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void TopRatedRestauarants_OnCall_ReturnsCorrectInformation()
        {
            var restaurant = new Restaurant
            {
                RestaurantId = Guid.Parse("a80298a8-0cf0-4196-9e8f-579956fa6a0b"),
                RestaurantPublicId = Guid.Parse("0632d258-ed85-4111-b90a-6f16b4aaaa9b"),
                AverageRating = 1.0,
                City = "Jake",
                Name = "Some Name",
                PhoneNumber = "1234567890",
                Reviews = new List<Review>(),
                State = "SA",
                Street = "123 Some St.",
                Website = "www.whatever.com"
            };

            _testContext.Restaurants.RemoveRange(_testContext.Restaurants);
            _testContext.Restaurants.Add(restaurant);
            _testContext.SaveChanges();


            var result = _controller.TopRatedRestaurants() as ViewResult;

            Approvals.VerifyAll(result.Model as IEnumerable<TopRatedRestaurantViewModel>, "TopRatedRestaurants:");
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void RestaurantDetails_OnPostGuid_ReturnsCorrectInformation()
        {
            var restaurant = new Restaurant
            {
                RestaurantId = Guid.Parse("846d69a2-b1be-4eb3-b563-20d86503f54e"),
                RestaurantPublicId = Guid.Parse("6ae03296-2bd6-4d52-a608-aa2f99ebaba4"),
                AverageRating = 1.0,
                City = "Some City",
                Name = "Some Name",
                PhoneNumber = "1234567890",
                Reviews = new List<Review>(),
                State = "SA",
                Street = "123 Some St.",
                Website = "www.whatever.com"
            };

            _testContext.Restaurants.RemoveRange(_testContext.Restaurants);
            _testContext.Restaurants.Add(restaurant);
            _testContext.SaveChanges();

            var result = _controller.RestaurantDetails(restaurant.RestaurantPublicId) as ViewResult;

            Approvals.Verify(result.Model);
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void RestaurantSearch_OnPostString_ReturnsCorrectInformation()
        {
            var restaurant = new Restaurant
            {
                RestaurantId = Guid.Parse("5495f6c7-7c97-4e29-9b19-b9bead7431df"),
                RestaurantPublicId = Guid.Parse("a80ae1c3-d37a-4820-9c3a-53164f6f50bc"),
                AverageRating = 1.0,
                City = "Jake",
                Name = "Some Name",
                PhoneNumber = "1234567890",
                Reviews = new List<Review>(),
                State = "SA",
                Street = "123 Some St.",
                Website = "www.whatever.com"
            };

            _testContext.Restaurants.RemoveRange(_testContext.Restaurants);
            _testContext.Restaurants.Add(restaurant);
            _testContext.SaveChanges();

            var result = _controller.RestaurantSearch("Jake") as ViewResult;

            Approvals.Verify(result.Model);
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void ViewReviews_OnPostViewModel_ReturnsCorrectInformation()
        {
            var restaurant = new Restaurant
            {
                RestaurantId = Guid.Parse("ac5cd95e-a038-4c8d-a664-dae78a867486"),
                RestaurantPublicId = Guid.Parse("db522ae1-3fd5-4f2e-a3b4-441beb42dd8a"),
                AverageRating = 1.0,
                City = "Jake",
                Name = "Some Name",
                PhoneNumber = "1234567890",
                Reviews = new List<Review>
                {
                    new Review
                    {
                        Comment = "Some Comment",
                        Rating = 1.0,
                        ReviewerName = "Some Name",
                        ReviewId = Guid.Parse("2986d9ff-e24e-4f86-af26-bc479fc4b348"),
                        ReviewPublicId = Guid.Parse("1d19d263-4425-499f-b857-5898e8edb2fa")
                    }
                },
                State = "SA",
                Street = "123 Some St.",
                Website = "www.whatever.com"
            };

            _testContext.Restaurants.RemoveRange(_testContext.Restaurants);
            _testContext.Restaurants.Add(restaurant);
            _testContext.SaveChanges();

            var result = _controller.ViewReviews(new ListRestaurantsViewModel {SelectRestaurantPublicId = restaurant.RestaurantPublicId}) as ViewResult;

            Approvals.Verify(result.Model);
        }

        [TestMethod]
        public void CreateRestaurant_OnPostViewModel_AddsRestaurant()
        {
            _testContext.Restaurants.RemoveRange(_testContext.Restaurants);
            _testContext.SaveChanges();

            var viewModel = new CreateRestaurantViewModel
            {
                City = "Some City",
                Name = "Some Name",
                PhoneNumber = "12345569",
                State = "SC",
                Street = "123 Blah St.",
                Website = "www.blah.com",
                ZipCode = 12345
            };

            var beforeAdd = _testContext.Restaurants.ToList();

            _controller.CreateRestaurant(viewModel);

            var afterAdd = _testContext.Restaurants.ToList();

            Assert.IsTrue(beforeAdd.Count + 1 == afterAdd.Count);
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void EditRestaurant_OnGetViewModel_ReturnsCorrectInformation()
        {
            var restaurant = new Restaurant
            {
                RestaurantId = Guid.Parse("caff9c9f-80f2-4f94-a3de-daf0e94e3aad"),
                RestaurantPublicId = Guid.Parse("0f9fb7b6-b4cc-4411-9500-0c521e6416fb"),
                AverageRating = 1.0,
                City = "Jake",
                Name = "Some Name",
                PhoneNumber = "1234567890",
                Reviews = new List<Review>(),
                State = "SA",
                Street = "123 Some St.",
                Website = "www.whatever.com"
            };

            _testContext.Restaurants.RemoveRange(_testContext.Restaurants);
            _testContext.Restaurants.Add(restaurant);
            _testContext.SaveChanges();

            var result = _controller.EditRestaurant(new ListRestaurantsViewModel{SelectRestaurantPublicId = restaurant.RestaurantPublicId}) as ViewResult;

            Approvals.Verify(result.Model.ToString());
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void EditRestaurant_OnPostViewModel_EditsRestaurant()
        {
            var builder = new StringBuilder();

            var restaurant = new Restaurant
            {
                RestaurantId = Guid.Parse("1173c864-11a5-49f0-9868-b39231603928"),
                RestaurantPublicId = Guid.Parse("0a4d887b-4d3a-4c25-83d8-d4d04ecd0042"),
                AverageRating = 1.0,
                City = "Jake",
                Name = "Some Name",
                PhoneNumber = "1234567890",
                Reviews = new List<Review>(),
                State = "SA",
                Street = "123 Some St.",
                Website = "www.whatever.com"
            };

            builder.Append(restaurant);

            _testContext.Restaurants.RemoveRange(_testContext.Restaurants);
            _testContext.Restaurants.Add(restaurant);
            _testContext.SaveChanges();

            _controller.EditRestaurant(new EditRestaurantViewModel
            {
                City = "New City",
                Name = "New Name",
                PhoneNumber = "09987654321",
                RestaurantPublicId = restaurant.RestaurantPublicId,
                State = "NS",
                Street = "123 New Street",
                Website = "www.new.com",
                ZipCode = 54321
            });

            var result = _testContext.Restaurants.Find(restaurant.RestaurantId);

            builder.Append(result);

            Approvals.Verify(builder.ToString());
        }

        [TestMethod]
        public void DeleteRestaurant_OnPostViewModel_DeletesRestaurant()
        {
            var restaurant = new Restaurant
            {
                RestaurantId = Guid.Parse("3ac27de7-98a9-468e-b840-5b54b3c0d3dd"),
                RestaurantPublicId = Guid.Parse("008ce260-dd8e-4968-a70b-26767b7c98fa"),
                AverageRating = 1.0,
                City = "Jake",
                Name = "Some Name",
                PhoneNumber = "1234567890",
                Reviews = new List<Review>
                {
                    
                },
                State = "SA",
                Street = "123 Some St.",
                Website = "www.whatever.com"
            };

            _testContext.Restaurants.RemoveRange(_testContext.Restaurants);
            _testContext.Restaurants.Add(restaurant);
            _testContext.SaveChanges();

            _controller.DeleteRestaurant(new ListRestaurantsViewModel {SelectRestaurantPublicId = restaurant.RestaurantPublicId});

            var retaurants = _testContext.Restaurants.ToList();

            Assert.IsFalse(retaurants.Exists(x => x.RestaurantPublicId == restaurant.RestaurantPublicId));
        }
    }
}
