using System;
using RR.DomainContracts;
using RR.Models;
using RR.RepositoryContracts;

namespace RR.DomainServices
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public void UpdateReview(Review review)
        {
            _reviewRepository.Update(review);
        }

        public void DeleteReview(Review review)
        {
            _reviewRepository.Delete(review);
        }

        public Review Get(Guid reviewPublicId)
        {
            return _reviewRepository.GetById(reviewPublicId);
        }
    }
}
