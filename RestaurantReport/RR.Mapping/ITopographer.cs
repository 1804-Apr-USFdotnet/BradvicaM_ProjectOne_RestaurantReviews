using RR.Models;
using RR.ViewModels;

namespace RR.Mapping
{
    public interface ITopographer
    {
        Review Map(EditReviewViewModel viewModel, Review review);
        Restaurant Map(EditRestaurantViewModel viewModel, Restaurant restaurant);
    }
}
