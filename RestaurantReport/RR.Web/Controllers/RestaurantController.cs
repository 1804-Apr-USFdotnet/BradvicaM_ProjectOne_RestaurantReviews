using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using RR.DomainContracts;
using RR.Models;
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
            var restaurants = _restaurantService.Get();

            var viewModel = _mapper.Map<ListRestaurantsViewModel>(restaurants);

            return View("ListRestaurants", viewModel);
        }

        //Change to get
        [Route("Restaurant/List")]
        [HttpGet]
        public ActionResult ListRestaurants(ListRestaurantsViewModel listRestaurantsViewModel)
        {
            var restaurants = _restaurantService.Get(listRestaurantsViewModel.ListOrder);

            var viewModel = _mapper.Map<ListRestaurantsViewModel>(restaurants);

            return View("ListRestaurants", viewModel);
        }

        [Route("Restaurant/TopRated")]
        public ActionResult TopRatedRestaurants()
        {
            var results = _restaurantService.TopThreeRatedRestaurants();

            var viewModel = _mapper.Map<IEnumerable<TopRatedRestaurantViewModel>>(results);

            return View("TopRatedRestaurants", viewModel);
        }

        //Change to get
        [Route("Restaurant/Details")]
        [HttpPost]
        public ActionResult RestaurantDetails(Guid restaurantId)
        {
            var restaurant = _restaurantService.Get(restaurantId);

            var viewModel = _mapper.Map<ViewRestaurantViewModel>(restaurant);

            return View("RestaurantDetails", viewModel);
        }

        //Change to get
        [Route("Restaurant/Search")]
        [HttpPost]
        public ActionResult RestaurantSearch(string searchTerm)
        {
            var results = _restaurantService.PartialSearch(searchTerm);

            var viewModel = _mapper.Map<ListRestaurantsViewModel>(results);

            return View("ListRestaurants", viewModel);
        }

        //Change to get
        [Route("Restaurant/Reviews")]
        [HttpPost]
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

        [Route("Restaurant/Create")]
        [HttpPost]
        public ActionResult CreateRestaurant(CreateRestaurantViewModel viewModel)
        {
            if (!ModelState.IsValid) return View("CreateRestaurant", viewModel);

            var restaurant = _mapper.Map<Restaurant>(viewModel);

            _restaurantService.CreateRestaurant(restaurant);

            return RedirectToAction("ListRestaurants");
        }

        [Route("Restaurant/Edit")]
        [HttpGet]
        public ActionResult EditRestaurant(ListRestaurantsViewModel postViewModel)
        {
            var restaurant = _restaurantService.Get(postViewModel.SelectRestaurantPublicId);

            var viewModel = _mapper.Map<EditRestaurantViewModel>(restaurant);

            return View("EditRestaurant", viewModel);
        }

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