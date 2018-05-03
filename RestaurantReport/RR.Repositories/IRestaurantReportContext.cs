using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using RR.Models;

namespace RR.Repositories
{
    public interface IRestaurantReportContext
    {
        DbSet<Review> Reviews { get; set; }
        DbSet<Restaurant> Restaurants { get; set; }
        int SaveChanges();
        DbEntityEntry Entry(object entity);
    }
}
