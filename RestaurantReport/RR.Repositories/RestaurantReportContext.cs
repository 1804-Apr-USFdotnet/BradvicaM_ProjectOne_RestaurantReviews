using System.Data.Entity;
using RR.Models;

namespace RR.Repositories
{
    public class RestaurantReportContext :DbContext, IContext
    {
        public RestaurantReportContext() : base("name=RestaurantReportConnectionString")
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
