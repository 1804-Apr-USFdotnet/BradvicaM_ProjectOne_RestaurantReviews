using System.ComponentModel.DataAnnotations;

namespace RR.ViewModels
{
    public class AddReviewViewModel
    {
        [Required]
        public double Rating { get; set; }

        public string Comment { get; set; }

        [Required]
        public string ReviewerName { get; set; }

        public RestaurantViewModel RestaurantViewModel { get; set; }
    }
}
