using System;
using System.Web.Mvc;
using AutoMapper;
using RR.DomainContracts;
using RR.Models;
using RR.ViewModels;
using RR.Web.Filter;

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

        [ExceptionFilter]
        [Route("Review/Edit")]
        [HttpGet]
        public ActionResult EditReview(RestaurantReviewsViewModel getViewModel)
        {
            var review = _reviewService.Get(getViewModel.SelectReviewPublicId);

            var viewModel = _mapper.Map<EditReviewViewModel>(review);

            return View("EditReview", viewModel);
        }

        [ExceptionFilter]
        [Route("Review/Edit")]
        [HttpPost]
        public ActionResult EditReview(EditReviewViewModel postViewModel)
        {
            if (!ModelState.IsValid) return View("EditReview", postViewModel);

            var review = _reviewService.Get(postViewModel.ReviewPublicId);

            var reviewToUpdate = _mapper.Map<Review>(new Tuple<EditReviewViewModel, Review>(postViewModel, review));
            
            _reviewService.UpdateReview(reviewToUpdate);

            return RedirectToAction("ListRestaurants", "Restaurant");
        }

        [ExceptionFilter]
        [Route("Review/Delete")]
        [HttpPost]
        public ActionResult DeleteReview(RestaurantReviewsViewModel postViewModel)
        {
            var review = _reviewService.Get(postViewModel.SelectReviewPublicId);

            _reviewService.DeleteReview(review);

            return RedirectToAction("ListRestaurants", "Restaurant");
        }

        [ExceptionFilter]
        [Route("Review/Create")]
        [HttpGet]
        public ActionResult CreateReview(ListRestaurantsViewModel getViewModel)
        {
            var viewModel = _mapper.Map<CreateReviewViewModel>(getViewModel);

            return View("CreateReview", viewModel);
        }

        [ExceptionFilter]
        [Route("Review/Create")]
        [HttpPost]
        public ActionResult CreateReview(CreateReviewViewModel postViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateReview", postViewModel);
            }

            var review = _mapper.Map<Review>(postViewModel);

            _reviewService.CreateReview(review, postViewModel.RestaurantPublicId);

            return RedirectToAction("ListRestaurants", "Restaurant");
        }
    }
}