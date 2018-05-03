using System;
using AutoMapper;
using RR.Models;
using RR.ViewModels;

namespace RR.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Restaurant, RestaurantViewModel>()
                .ForSourceMember(x => x.RestaurantId, opt => opt.Ignore());

            CreateMap<Restaurant, RestaurantNameViewModel>()
                .ForSourceMember(x => x.RestaurantId, opt => opt.Ignore())
                .ForSourceMember(x => x.AverageRating, opt => opt.Ignore())
                .ForSourceMember(x => x.City, opt => opt.Ignore())
                .ForSourceMember(x => x.PhoneNumber, opt => opt.Ignore())
                .ForSourceMember(x => x.State, opt => opt.Ignore())
                .ForSourceMember(x => x.Street, opt => opt.Ignore())
                .ForSourceMember(x => x.Website, opt => opt.Ignore())
                .ForSourceMember(x => x.ZipCode, opt => opt.Ignore());

            CreateMap<CreateReviewViewModel, Review>()
                .ForMember(des => des.PublicId, opt => opt.UseValue(Guid.NewGuid()))
                .ForMember(des => des.ReviewId, opt => opt.UseValue(Guid.NewGuid()))
                .ForMember(des => des.RestaurantId, opt => opt.MapFrom(des => des.Restaurant.RestaurantId));

            CreateMap<Review, ReviewViewModel>()
                .ForSourceMember(src => src.ReviewId, opt => opt.Ignore())
                .ForSourceMember(src => src.Restaurant, opt => opt.Ignore())
                .ForSourceMember(src => src.RestaurantId, opt => opt.Ignore());

            CreateMap<EditReviewViewModel, Review>()
                .ForMember(des => des.ReviewId, opt => opt.MapFrom(src => src.Review.ReviewId))
                .ForMember(des => des.RestaurantId, opt => opt.MapFrom(src => src.Review.RestaurantId))
                .ForMember(des => des.Restaurant, opt => opt.MapFrom(src => src.Review.Restaurant));

            CreateMap<Restaurant, TopRatedRestaurantsViewModel>()
                .ForSourceMember(x => x.RestaurantId, opt => opt.Ignore())
                .ForSourceMember(x => x.City, opt => opt.Ignore())
                .ForSourceMember(x => x.PhoneNumber, opt => opt.Ignore())
                .ForSourceMember(x => x.State, opt => opt.Ignore())
                .ForSourceMember(x => x.Street, opt => opt.Ignore())
                .ForSourceMember(x => x.Website, opt => opt.Ignore())
                .ForSourceMember(x => x.ZipCode, opt => opt.Ignore());

            CreateMap<CreateRestaurantViewModel, Restaurant>()
                .ForMember(des => des.RestaurantId, opt => opt.UseValue(Guid.NewGuid()))
                .ForMember(des => des.PublicId, opt => opt.UseValue(Guid.NewGuid()))
                .ForMember(x => x.AverageRating, opt => opt.Ignore())
                .ForMember(x => x.Reviews, opt => opt.Ignore());

            CreateMap<EditRestaurantViewModel, Restaurant>()
                .ForMember(des => des.RestaurantId, opt => opt.MapFrom(src => src.Restaurant.RestaurantId))
                .ForMember(x => x.AverageRating, opt => opt.MapFrom(src => src.Restaurant.AverageRating))
                .ForMember(x => x.Reviews, opt => opt.MapFrom(src => src.Restaurant.Reviews));

        }
    }
}
