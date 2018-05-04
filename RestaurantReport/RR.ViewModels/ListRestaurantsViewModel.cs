using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RR.ViewModels
{
    public class ListRestaurantsViewModel
    {
        public string ListOrder { get; set; }
        public Guid SelectRestaurantPublicId { get; set; }
        public IEnumerable<SelectListItem> SelectListItems { get; }
        public IEnumerable<ViewRestaurantViewModel> ViewRestaurantViewModels { get; set; }

        public ListRestaurantsViewModel()
        {
            SelectListItems = new List<SelectListItem>
            {
                new SelectListItem{Text = "Names", Value = "name"},
                new SelectListItem{Text = "State", Value = "state"},
                new SelectListItem{Text = "City", Value = "city"},
                new SelectListItem{Text = "Rating", Value = "rating"}
            };
        }
    }
}
