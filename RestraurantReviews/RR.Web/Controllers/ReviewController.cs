using System;
using System.Web.Mvc;
using AutoMapper;
using RR.DomainContracts;
using RR.ViewModels;

namespace RR.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;

        public ReviewController(IReviewService reviewService, IRestaurantService restaurantService, IMapper mapper)
        {
            _reviewService = reviewService;
            _restaurantService = restaurantService;
            _mapper = mapper;
        }

        //FIX THIS JUNK, USE CreateReviewViewModel, Dump viewdata.
        [Route("Review/New")]
        [HttpPost]
        public ActionResult NewReview(Guid restaurantId)
        {
            ViewData["Id"] = restaurantId;
            return View();
        }

        
        [Route("Review/Create")]
        [HttpPost]
        public ActionResult CreateReview(CreateReviewViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            return RedirectToAction("ListRestaurants", "Restaurant");    
        }

        [Route("Review/Edit")]
        [HttpPost]
        public ActionResult EditReview(ReviewViewModel viewModel)
        {
            return null;
        }
    }
}