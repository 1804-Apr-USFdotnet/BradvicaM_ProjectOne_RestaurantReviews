using System;
using RR.DomainContracts;
using RR.Models;
using RR.RepositoryContracts;

namespace RR.DomainServices
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public ReviewService(IReviewRepository reviewRepository, IRestaurantRepository restaurantRepository)
        {
            _reviewRepository = reviewRepository;
            _restaurantRepository = restaurantRepository;
        }

        public void UpdateReview(Review review)
        {
            var restaurant = review.Restaurant;
            _reviewRepository.Update(review);

            restaurant.CalculateAverageRating(restaurant.Reviews);
            _restaurantRepository.Update(restaurant);
        }

        public void DeleteReview(Review review)
        {
            var restaurant = review.Restaurant;
            _reviewRepository.Delete(review);

            restaurant.CalculateAverageRating(restaurant.Reviews);
            _restaurantRepository.Update(restaurant);
        }

        public Review Get(Guid reviewPublicId)
        {
            return _reviewRepository.GetById(reviewPublicId);
        }
    }
}
