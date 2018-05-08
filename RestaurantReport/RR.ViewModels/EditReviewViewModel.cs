using System;
using System.ComponentModel.DataAnnotations;

namespace RR.ViewModels
{
    public class EditReviewViewModel
    {
        public Guid ReviewPublicId { get; set; }

        [Required]
        public string ReviewerName { get; set; }

        [Required]
        [Range(1, 10)]
        public double Rating { get; set; }

        public string Comment { get; set; }

        public override string ToString()
        {
            return $"\nPublicId: {ReviewPublicId}\nReviewerName: {ReviewerName}\nRating: {Rating}\nComment: {Comment}\n";
        }
    }
}
