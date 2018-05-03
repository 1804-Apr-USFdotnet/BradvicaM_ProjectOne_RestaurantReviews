using System;
using RR.DomainContracts;
using RR.Models;
using RR.RepositoryContracts;

namespace RR.DomainServices
{
    public class ReviewService : IReviewService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository, IRestaurantRepository restaurantRepository)
        {
            _reviewRepository = reviewRepository;
            _restaurantRepository = restaurantRepository;
        }

        public void AddReview(Review review)
        {
            var restaurant = _restaurantRepository.GetByName(review.Restaurant.Name);

            review.Restaurant = restaurant;
            _reviewRepository.Add(review);

            restaurant.CalculateAverageRating(restaurant.Reviews);
            _restaurantRepository.UpdateRestaurant();
        }

        public Review GetById(Guid id)
        {
            return _reviewRepository.GetById(id);
        }

        public void UpdateReview(Review review)
        {
            _reviewRepository.Update(review);
        }

        public void DeleteReview(Review review)
        {
            _reviewRepository.Delete(review);
        }
    }
}
