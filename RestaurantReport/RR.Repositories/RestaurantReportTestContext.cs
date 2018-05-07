using System.Data.Entity;
using RR.Models;

namespace RR.Repositories
{
    public class RestaurantReportTestContext : DbContext, IContext
    {
        public RestaurantReportTestContext() : base("name=RestaurantReportTestConnectionString")
        {
            Database.SetInitializer(new TestDatabaseInitializer());
        }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
