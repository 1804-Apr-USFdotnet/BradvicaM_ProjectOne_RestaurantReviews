using System;
using System.ComponentModel.DataAnnotations;

namespace RR.ViewModels
{
    public class CreateReviewViewModel
    {
        public Guid RestaurantPublicId { get; set; }

        [Required]
        public string ReviewerName { get; set; }

        [Required]
        [Range(1, 10)]
        public double Rating { get; set; }

        public string Comment { get; set; }
    }
}
