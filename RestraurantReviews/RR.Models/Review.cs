using System;

namespace RR.Models
{
    public class Review
    {
        public Guid ReviewId { get; set; }
        public Guid PublicId { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public string ReviewerName { get; set; }

        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public override string ToString()
        {
            return $"\nReviewId: {ReviewId}\nRating: {Rating}\nComment: {Comment}\nName: {ReviewerName}\nId: {PublicId}\n";
        }
    }
}
