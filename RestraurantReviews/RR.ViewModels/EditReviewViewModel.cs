using System;
using RR.Models;

namespace RR.ViewModels
{
    public class EditReviewViewModel
    {
        public Review Review { get; set; }
        public Guid PublicId { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public string ReviewerName { get; set; }
    }
}
