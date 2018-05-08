using System;

namespace RR.ViewModels
{
    public class EditReviewViewModel
    {
        public Guid ReviewPublicId { get; set; }
        public string ReviewerName { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }

        public override string ToString()
        {
            return $"\nPublicId: {ReviewPublicId}\nReviewerName: {ReviewerName}\nRating: {Rating}\nComment: {Comment}\n";
        }
    }
}
