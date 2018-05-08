using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RR.Models;
using RR.ViewModels;

namespace RR.Mapping
{
    public class MappingProfile : Profile
    {
        

        public MappingProfile()
        {
            CreateMap<Restaurant, ViewRestaurantViewModel>();

            CreateMap<Restaurant, TopRatedRestaurantViewModel>();

            CreateMap<Review, ViewReviewViewModel>();

            CreateMap<CreateRestaurantViewModel, Restaurant>()
                .BeforeMap((s, d) => d.RestaurantId = Guid.NewGuid())
                .BeforeMap((s, d) => d.RestaurantPublicId = Guid.NewGuid())
                .ForMember(des => des.Reviews, opt => opt.Ignore())
                .ForMember(des => des.AverageRating, opt => opt.Ignore())
                .ForMember(d => d.RestaurantId, o => o.Ignore())
                .ForMember(d => d.RestaurantPublicId, o => o.Ignore());

            CreateMap<ListRestaurantsViewModel, CreateReviewViewModel>()
                .ForMember(des => des.Rating, opt => opt.Ignore())
                .ForMember(des => des.Comment, opt => opt.Ignore())
                .ForMember(des => des.ReviewerName, opt => opt.Ignore())
                .ForMember(des => des.RestaurantPublicId, opt => opt.MapFrom(src => src.SelectRestaurantPublicId));

            CreateMap<CreateReviewViewModel, Review>()
                .BeforeMap((s, d) => d.ReviewId = Guid.NewGuid())
                .BeforeMap((s, d) => d.ReviewPublicId = Guid.NewGuid())
                .ForMember(des => des.RestaurantId, opt => opt.Ignore())
                .ForMember(des => des.Restaurant, opt => opt.Ignore())
                .ForMember(d => d.ReviewId, o => o.Ignore())
                .ForMember(d => d.ReviewPublicId, o => o.Ignore());

            CreateMap<Restaurant, EditRestaurantViewModel>();

            CreateMap<EditRestaurantViewModel, Restaurant>()
                .ForMember(des => des.RestaurantId, opt => opt.Ignore())
                .ForMember(des => des.AverageRating, opt => opt.Ignore())
                .ForMember(des => des.Reviews, opt => opt.Ignore());

            CreateMap<Review, EditReviewViewModel>();

            CreateMap<Tuple<EditReviewViewModel, Review>, Review>()
                .ForMember(d => d.ReviewId, o => o.MapFrom(s => s.Item2.ReviewId))
                .ForMember(d => d.ReviewPublicId, o => o.MapFrom(s => s.Item2.ReviewPublicId))
                .ForMember(d => d.ReviewerName, o => o.MapFrom(s => s.Item1.ReviewerName))
                .ForMember(d => d.Rating, o => o.MapFrom(s => s.Item1.Rating))
                .ForMember(d => d.Comment, o => o.MapFrom(s => s.Item1.Comment))
                .ForMember(d => d.RestaurantId, o => o.MapFrom(s => s.Item2.RestaurantId))
                .ForMember(d => d.Restaurant, o => o.MapFrom(s => s.Item2.Restaurant));

            CreateMap<Tuple<EditRestaurantViewModel, Restaurant>, Restaurant>()
                .ForMember(d => d.RestaurantId, o => o.MapFrom(s => s.Item2.RestaurantId))
                .ForMember(d => d.RestaurantPublicId, o => o.MapFrom(s => s.Item2.RestaurantPublicId))
                .ForMember(d => d.AverageRating, o => o.MapFrom(s => s.Item2.AverageRating))
                .ForMember(d => d.Reviews, o => o.MapFrom(s => s.Item2.Reviews))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Item1.Name))
                .ForMember(d => d.Street, o => o.MapFrom(s => s.Item1.Street))
                .ForMember(d => d.City, o => o.MapFrom(s => s.Item1.City))
                .ForMember(d => d.State, o => o.MapFrom(s => s.Item1.State))
                .ForMember(d => d.ZipCode, o => o.MapFrom(s => s.Item1.ZipCode))
                .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.Item1.PhoneNumber))
                .ForMember(d => d.Website, o => o.MapFrom(s => s.Item1.Website));

            CreateMap<IEnumerable<ViewRestaurantViewModel>, ListRestaurantsViewModel>()
                .ForMember(d => d.ViewRestaurantViewModels, o => o.MapFrom(s => s.ToList()))
                .ForMember(d => d.ListOrder, o => o.Ignore())
                .ForMember(d => d.SelectListItems, o => o.Ignore())
                .ForMember(d => d.SelectRestaurantPublicId, o => o.Ignore());

            CreateMap<Tuple<IEnumerable<ViewReviewViewModel>, ViewRestaurantViewModel>, RestaurantReviewsViewModel>()
                .ForMember(d => d.Restaurant, o => o.MapFrom(s => s.Item2))
                .ForMember(d => d.Reviews, o => o.MapFrom(s => s.Item1))
                .ForMember(d => d.SelectReviewPublicId, o => o.Ignore());

            CreateMap<Restaurant, RestaurantReviewsViewModel>()
                .ForMember(d => d.Reviews, o => o.MapFrom(s => s.Reviews))
                .ForMember(d => d.SelectReviewPublicId, o => o.Ignore())
                .ForMember(d => d.Restaurant, o => o.MapFrom(s => s));

            CreateMap<IEnumerable<Restaurant>, ListRestaurantsViewModel>()
                .ForMember(d => d.ViewRestaurantViewModels, o => o.MapFrom(s => s))
                .ForMember(d => d.ListOrder, o => o.Ignore())
                .ForMember(d => d.SelectListItems, o => o.Ignore())
                .ForMember(d => d.SelectRestaurantPublicId, o => o.Ignore());
        }
    }
}
