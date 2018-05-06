using AutoMapper;
using RR.Models;
using RR.ViewModels;

namespace RR.Mapping
{
    public class Topographer : ITopographer
    {
        private readonly IMapper _mapper;

        public Topographer(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Review Map(EditReviewViewModel viewModel, Review review)
        {
            var mappedReview = _mapper.Map<EditReviewViewModel, Review>(viewModel);

            mappedReview.Restaurant = review.Restaurant;
            mappedReview.ReviewId = review.ReviewId;
            mappedReview.RestaurantId = review.RestaurantId;

            return mappedReview;
        }

        public Restaurant Map(EditRestaurantViewModel viewModel, Restaurant restaurant)
        {
            var mappedRestaurant = _mapper.Map<EditRestaurantViewModel, Restaurant>(viewModel);
            mappedRestaurant.RestaurantId = restaurant.RestaurantId;
            mappedRestaurant.Reviews = restaurant.Reviews;
            mappedRestaurant.AverageRating = restaurant.AverageRating;

            return mappedRestaurant;
        }
    }
}
