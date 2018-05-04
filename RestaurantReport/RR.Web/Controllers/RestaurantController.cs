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

        [Route("Restaurant/Search")]
        [HttpPost]
        public ActionResult RestaurantSearch(string searchTerm)
        {
            var results = _restaurantService.PartialSearch(searchTerm);

            var restaurantViewModels = _mapper.Map<IEnumerable<ViewRestaurantViewModel>>(results);

            var viewModel = new ListRestaurantsViewModel{ViewRestaurantViewModels = restaurantViewModels};

            return View("ListRestaurants", viewModel);
        }

        [Route("Restaurant/Reviews")]
        [HttpPost]
        public ActionResult ViewReviews(ListRestaurantsViewModel postViewModel)
        {
            var restaurant = _restaurantService.Get(postViewModel.SelectRestaurantPublicId);

            var reviewsVM = _mapper.Map<IEnumerable<Review>, IEnumerable<ViewReviewViewModel>>(restaurant.Reviews);
            var restaurantVM = _mapper.Map<Restaurant, ViewRestaurantViewModel>(restaurant);

            var viewModel = new RestaurantReviewsViewModel{Restaurant =  restaurantVM, Reviews = reviewsVM};

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

        [Route("Restaurant/Review")]
        [HttpGet]
        public ActionResult CreateReview(ListRestaurantsViewModel getViewModel)
        {
            var viewModel = _mapper.Map<ListRestaurantsViewModel, CreateReviewViewModel>(getViewModel);

            return View("CreateReview", viewModel);
        }

        [Route("Restaurant/Review")]
        [HttpPost]
        public ActionResult CreateReview(CreateReviewViewModel postViewModel)
        {
            if (!ModelState.IsValid) return View("CreateReview", postViewModel);

            //var restaurant = _restaurantService.Get(postViewModel.RestaurantPublicId);

            var review = _mapper.Map<Review>(postViewModel);

            //review.Restaurant = restaurant;

            _restaurantService.ReviewRestaurant(review);

            return RedirectToAction("ListRestaurants");
        }
    }
}