using System;

namespace RR.Models
{
    public class Review
    {
        public Guid ReviewId { get; set; }
        public Guid ReviewPublicId { get; set; }
        public string ReviewerName { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }

        public Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        public override string ToString()
        {
            return $"\nReviewId: {ReviewId}\nId: {ReviewPublicId}\nName: {ReviewerName}\nRating: {Rating}\nComment: {Comment}\n";
        }
    }
}
