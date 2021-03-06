﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.Models;
using RR.QueryObjects;

namespace RR.Tests.Unit.QueryObjects
{
    [TestClass]
    public class PartialSearchQueryTests
    {
        private List<Restaurant> _restaurants;

        [TestMethod]
        public void AsExpression_Returns_RestaurntWithMatchingString()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    City = "Kansas City",
                    State = "MO",
                    Street = "123 Barbeque St.",
                    ZipCode = 81721,
                    RestaurantPublicId = Guid.Empty,
                    Name = "Billy bobby Texas BBQ",
                    PhoneNumber = "8761234121",
                    AverageRating = 0.0,
                    Website = "www.billybobs.com"
                },
                new Restaurant
                {
                    City = "Kansas City",
                    State = "MO",
                    Street = "123 Barbeque St.",
                    ZipCode = 81721,
                    RestaurantPublicId = Guid.Empty,
                    Name = "Billy Bobs Texas BBQ",
                    PhoneNumber = "8761234121",
                    AverageRating = 0.0,
                    Website = "www.billybobs.com"
                }
            };

            const string queryValue = "bobby";

            var query = new PartialSearchQuery(queryValue, _restaurants);

            var results = query.AsExpression();

            Assert.IsTrue(results.Count == 1);
        }

        [TestMethod]
        public void AsExpression_DoesNotReturn_RestaurantWithNoMatchingString()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    City = "Kansas City",
                    State = "MO",
                    Street = "123 Barbeque St.",
                    ZipCode = 81721,
                    RestaurantPublicId = Guid.Empty,
                    Name = "Billy johns Texas BBQ",
                    PhoneNumber = "8761234121",
                    AverageRating = 0.0,
                    Website = "www.billybobs.com"
                },
                new Restaurant
                {
                    City = "Kansas City",
                    State = "MO",
                    Street = "123 Barbeque St.",
                    ZipCode = 81721,
                    RestaurantPublicId = Guid.Empty,
                    Name = "Billy bob Texas BBQ",
                    PhoneNumber = "8761234121",
                    AverageRating = 0.0,
                    Website = "www.billybobs.com"
                }
            };

            const string queryValue = "MO";

            var query = new PartialSearchQuery(queryValue, _restaurants);

            var results = query.AsExpression();

            Assert.IsTrue(results.Count == 2);
        }
    }
}
