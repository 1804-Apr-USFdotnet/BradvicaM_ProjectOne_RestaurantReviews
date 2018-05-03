using System.Data.Entity;
using RR.Models;

namespace RR.Repositories
{
    public class RestaurantReportContext : DbContext, IRestaurantReportContext
    { 
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
