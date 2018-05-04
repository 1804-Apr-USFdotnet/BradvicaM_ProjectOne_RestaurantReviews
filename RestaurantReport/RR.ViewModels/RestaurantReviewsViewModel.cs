using System.Collections.Generic;

namespace RR.ViewModels
{
    public class RestaurantReviewsViewModel
    {
        public IEnumerable<ViewReviewViewModel> Reviews { get; set; }
        public ViewRestaurantViewModel Restaurant { get; set; }
    }
}
