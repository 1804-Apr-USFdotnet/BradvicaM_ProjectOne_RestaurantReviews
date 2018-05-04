using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using RR.DomainContracts;
using RR.ViewModels;

namespace RR.Web.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;

        public RestaurantController(IRestaurantService restaurantService, IMapper mapper)
        {
            _restaurantService = restaurantService;
            _mapper = mapper;
        }

        [Route("Restaurant/List")]
        [HttpGet]
        public ActionResult ListRestaurants()
        {
            var restaurantViewModels = _mapper.Map<IEnumerable<ViewRestaurantViewModel>>(_restaurantService.Get());

            var viewModel = new ListRestaurantsViewModel{ViewRestaurantViewModels = restaurantViewModels};

            return View("ListRestaurants", viewModel);
        }

        [Route("Restaurant/List")]
        [HttpPost]
        public ActionResult ListRestaurants(ListRestaurantsViewModel listRestaurantsViewModel)
        {
            var results = _restaurantService.Get(listRestaurantsViewModel.ListOrder);

            var restaurantViewModels = _mapper.Map<IEnumerable<ViewRestaurantViewModel>>(results);

            var viewModel = new ListRestaurantsViewModel { ViewRestaurantViewModels = restaurantViewModels };

            return View(viewModel);
        }
    }
}