using System.Web.Mvc;
using AutoMapper;
using RR.DomainContracts;
using RR.Models;
using RR.ViewModels;

namespace RR.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        [Route("Review/Edit")]
        [HttpGet]
        public ActionResult EditReview(RestaurantReviewsViewModel getViewModel)
        {
            var review = _reviewService.Get(getViewModel.SelectReviewPublicId);

            var viewModel = _mapper.Map<Review, EditReviewViewModel>(review);

            return View("EditReview", viewModel);
        }

        [Route("Review/Edit")]
        [HttpPost]
        public ActionResult EditReview(EditReviewViewModel postViewModel)
        {
            if (!ModelState.IsValid) return View("EditReview", postViewModel);

            var review = _reviewService.Get(postViewModel.ReviewPublicId);

            //Complex Mapper
            var reviewToUpdate = _mapper.Map<EditReviewViewModel, Review>(postViewModel);
            reviewToUpdate.Restaurant = review.Restaurant;
            reviewToUpdate.ReviewId = review.ReviewId;
            reviewToUpdate.RestaurantId = review.RestaurantId;

            _reviewService.UpdateReview(reviewToUpdate);

            return RedirectToAction("ListRestaurants", "Restaurant");
        }

        [Route("Review/Delete")]
        [HttpPost]
        public ActionResult DeleteReview(RestaurantReviewsViewModel postViewModel)
        {
            var review = _reviewService.Get(postViewModel.SelectReviewPublicId);

            _reviewService.DeleteReview(review);

            return RedirectToAction("ListRestaurants", "Restaurant");
        }

        [Route("Review/Create")]
        [HttpGet]
        public ActionResult CreateReview(ListRestaurantsViewModel getViewModel)
        {
            var viewModel = _mapper.Map<ListRestaurantsViewModel, CreateReviewViewModel>(getViewModel);

            return View("CreateReview", viewModel);
        }

        [Route("Review/Create")]
        [HttpPost]
        public ActionResult CreateReview(CreateReviewViewModel postViewModel)
        {
            if (!ModelState.IsValid) return View("CreateReview", postViewModel);

            var review = _mapper.Map<Review>(postViewModel);

            _reviewService.CreateReview(review, postViewModel.RestaurantPublicId);

            return RedirectToAction("ListRestaurants", "Restaurant");
        }
    }
}