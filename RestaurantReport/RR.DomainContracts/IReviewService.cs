using System;
using RR.Models;

namespace RR.DomainContracts
{
    public interface IReviewService
    {
        void UpdateReview(Review review);
        void DeleteReview(Review review);
        void CreateReview(Review review, Guid restaurantPublicId);
        Review Get(Guid reviewPublicId);
    }
}
