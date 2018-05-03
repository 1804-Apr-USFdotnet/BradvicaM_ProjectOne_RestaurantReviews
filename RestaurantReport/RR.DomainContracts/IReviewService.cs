using RR.Models;

namespace RR.DomainContracts
{
    public interface IReviewService
    {
        void CreateReview(Review review);
        void UpdateReview(Review review);
        void DeleteReview(Review review);
    }
}
