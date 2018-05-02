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

        [Route("Review/Create")]
        [HttpPost]
        public ActionResult CreateReview(AddReviewViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            return View();
        }

        [Route("Review/Edit")]
        [HttpPost]
        public ActionResult EditReview(ReviewViewModel viewModel)
        {
            return null;
        }
    }
}