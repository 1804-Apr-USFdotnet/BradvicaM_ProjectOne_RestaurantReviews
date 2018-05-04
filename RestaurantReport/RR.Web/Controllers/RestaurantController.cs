using System;
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

        [Route("Restaurant/Index")]
        public ActionResult Index()
        {
            return View("Index");
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

            return View("ListRestaurants", viewModel);
        }

        [Route("Restaurant/TopRated")]
        public ActionResult TopRatedRestaurants()
        {
            var results = _restaurantService.TopThreeRatedRestaurants();

            var viewModel = _mapper.Map<IEnumerable<TopRatedRestaurantViewModel>>(results);

            return View("TopRatedRestaurants", viewModel);
        }

        [Route("Restaurant/Details")]
        [HttpPost]
        public ActionResult RestaurantDetails(Guid restaurantId)
        {
            var restaurant = _restaurantService.Get(restaurantId);

            var viewModel = _mapper.Map<ViewRestaurantViewModel>(restaurant);

            return View("RestaurantDetails", viewModel);
        }
    }
}