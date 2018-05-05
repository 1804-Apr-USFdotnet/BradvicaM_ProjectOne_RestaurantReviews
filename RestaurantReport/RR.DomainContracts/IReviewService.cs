using System;
using RR.Models;

namespace RR.DomainContracts
{
    public interface IReviewService
    {
        void UpdateReview(Review review);
        void DeleteReview(Review review);
        Review Get(Guid reviewPublicId);
    }
}
