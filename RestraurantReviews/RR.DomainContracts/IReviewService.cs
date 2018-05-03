using System;
using RR.Models;

namespace RR.DomainContracts
{
    public interface IReviewService
    {
        void AddReview(Review review);
        Review GetById(Guid id);
        void UpdateReview(Review review);
        void DeleteReview(Review review);
    }
}
