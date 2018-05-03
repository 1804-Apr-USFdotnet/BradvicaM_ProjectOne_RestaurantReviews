using System;
using System.ComponentModel.DataAnnotations;
using RR.Models;

namespace RR.ViewModels
{
    public class CreateReviewViewModel
    {
        [Required]
        public double Rating { get; set; }

        public string Comment { get; set; }

        [Required]
        public string ReviewerName { get; set; }

        public Guid RestaurantPublicId { get; set; }

        public Restaurant Restaurant { get; set; }
    }
}
