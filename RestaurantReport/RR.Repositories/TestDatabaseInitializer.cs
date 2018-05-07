using System;
using System.Collections.Generic;
using System.Data.Entity;
using RR.Models;

namespace RR.Repositories
{
    public class TestDatabaseInitializer : CreateDatabaseIfNotExists<RestaurantReportTestContext>
    {
        protected override void Seed(RestaurantReportTestContext context)
        {
            var restaurant = new Restaurant
            {
                City = "Kansas City",
                State = "MO",
                Street = "123 Barbeque St.",
                ZipCode = 81721,
                RestaurantPublicId = Guid.NewGuid(),
                RestaurantId = Guid.NewGuid(),
                Name = "Billy Bobs Texas BBQ",
                PhoneNumber = "8761234121",
                AverageRating = 0.0,
                Website = "www.billybobs.com",
                Reviews = new List<Review>()
            };

            var review = new Review
            {
                ReviewPublicId = Guid.NewGuid(),
                ReviewId = Guid.NewGuid(),
                Comment = "It was ok.",
                Rating = 5.00,
                ReviewerName = "Mike"
            };

            restaurant.Reviews.Add(review);

            context.Restaurants.Add(restaurant);
            base.Seed(context);
            context.SaveChanges();
        }
    }
}
