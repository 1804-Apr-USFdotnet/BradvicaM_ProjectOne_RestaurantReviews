using System;

namespace RR.ViewModels
{
    public class EditReviewViewModel
    {
        public Guid ReviewPublicId { get; set; }
        public string ReviewerName { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
    }
}
