using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using RR.DomainContracts;
using RR.Models;
using RR.ViewModels;
using RR.Web.Filter;

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

        [ExceptionFilter]
        [Route("Restaurant/List")]
        [HttpGet]
        public ActionResult ListRestaurants()
        {
            var restaurants = _restaurantService.Get();

            var viewModel = _mapper.Map<ListRestaurantsViewModel>(restaurants);

            return View("ListRestaurants", viewModel);
        }

        [ExceptionFilter]
        [Route("Restaurant/OrderList")]
        [HttpGet]
        public ActionResult OrderListRestaurants(ListRestaurantsViewModel listRestaurantsViewModel)
        {
            var restaurants = _restaurantService.Get(listRestaurantsViewModel.ListOrder);

            var viewModel = _mapper.Map<ListRestaurantsViewModel>(restaurants);

            return View("ListRestaurants", viewModel);
        }

        [ExceptionFilter]
        [Route("Restaurant/TopRated")]
        public ActionResult TopRatedRestaurants()
        {
            var results = _restaurantService.TopThreeRatedRestaurants();

            var viewModel = _mapper.Map<IEnumerable<TopRatedRestaurantViewModel>>(results);

            return View("TopRatedRestaurants", viewModel);
        }

        [ExceptionFilter]
        [Route("Restaurant/Details")]
        [HttpGet]
        public ActionResult RestaurantDetails(Guid restaurantId)
        {
            var restaurant = _restaurantService.Get(restaurantId);

            var viewModel = _mapper.Map<ViewRestaurantViewModel>(restaurant);

            return View("RestaurantDetails", viewModel);
        }

        [ExceptionFilter]
        [Route("Restaurant/Search")]
        [HttpGet]
        public ActionResult RestaurantSearch(string searchTerm)
        {
            var results = _restaurantService.PartialSearch(searchTerm);

            var viewModel = _mapper.Map<ListRestaurantsViewModel>(results);

            return View("ListRestaurants", viewModel);
        }

        [ExceptionFilter]
        [Route("Restaurant/Reviews")]
        [HttpGet]
        public ActionResult ViewReviews(ListRestaurantsViewModel postViewModel)
        {
            var restaurant = _restaurantService.Get(postViewModel.SelectRestaurantPublicId);

            var viewModel = _mapper.Map<RestaurantReviewsViewModel>(restaurant);
                
            return View("ViewReviews", viewModel);
        }

        [Route("Restaurant/Create")]
        [HttpGet]
        public ActionResult CreateRestaurant()
        {
            return View("CreateRestaurant");
        }

        [ExceptionFilter]
        [Route("Restaurant/Create")]
        [HttpPost]
        public ActionResult CreateRestaurant(CreateRestaurantViewModel viewModel)
        {
            if (!ModelState.IsValid) return View("CreateRestaurant", viewModel);

            var restaurant = _mapper.Map<Restaurant>(viewModel);

            _restaurantService.CreateRestaurant(restaurant);

            return RedirectToAction("ListRestaurants");
        }

        [ExceptionFilter]
        [Route("Restaurant/Edit")]
        [HttpGet]
        public ActionResult EditRestaurant(ListRestaurantsViewModel postViewModel)
        {
            var restaurant = _restaurantService.Get(postViewModel.SelectRestaurantPublicId);

            var viewModel = _mapper.Map<EditRestaurantViewModel>(restaurant);

            return View("EditRestaurant", viewModel);
        }

        [ExceptionFilter]
        [Route("Restaurant/Edit")]
        [HttpPost]
        public ActionResult EditRestaurant(EditRestaurantViewModel postViewModel)
        {
            if (!ModelState.IsValid) return View("EditRestaurant", postViewModel);

            var restaurant = _restaurantService.Get(postViewModel.RestaurantPublicId);

            var restaurantToUpdate = _mapper.Map<Restaurant>(new Tuple<EditRestaurantViewModel, Restaurant>(postViewModel, restaurant));
            
            _restaurantService.UpdateRestaurant(restaurantToUpdate);

            return RedirectToAction("ListRestaurants");
        }

        [ExceptionFilter]
        [Route("Restaurant/Delete")]
        [HttpPost]
        public ActionResult DeleteRestaurant(ListRestaurantsViewModel postViewModel)
        {
            var restaurant = _restaurantService.Get(postViewModel.SelectRestaurantPublicId);

            _restaurantService.DeleteRestaurant(restaurant);

            return RedirectToAction("ListRestaurants");
        }
    }
}