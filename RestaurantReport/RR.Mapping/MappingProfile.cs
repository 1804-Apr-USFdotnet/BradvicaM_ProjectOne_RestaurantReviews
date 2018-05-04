using System;
using System.Runtime.Remoting.Channels;
using AutoMapper;
using RR.Models;
using RR.ViewModels;

namespace RR.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Restaurant, ViewRestaurantViewModel>()
                .ForSourceMember(src => src.RestaurantId, opt => opt.Ignore());

            CreateMap<Restaurant, TopRatedRestaurantViewModel>()
                .ForSourceMember(src => src.RestaurantId, opt => opt.Ignore())
                .ForSourceMember(src => src.Website, opt => opt.Ignore())
                .ForSourceMember(src => src.PhoneNumber, opt => opt.Ignore())
                .ForSourceMember(src => src.Reviews, opt => opt.Ignore());

            CreateMap<Review, ViewReviewViewModel>()
                .ForSourceMember(src => src.ReviewId, opt => opt.Ignore())
                .ForSourceMember(src => src.RestaurantId, opt => opt.Ignore())
                .ForSourceMember(src => src.Restaurant, opt => opt.Ignore());

            CreateMap<CreateRestaurantViewModel, Restaurant>()
                .ForMember(des => des.Reviews, opt => opt.Ignore())
                .ForMember(des => des.AverageRating, opt => opt.Ignore())
                .ForMember(des => des.RestaurantId, opt => opt.UseValue(Guid.NewGuid()))
                .ForMember(des => des.RestaurantPublicId, opt => opt.UseValue(Guid.NewGuid()));

            CreateMap<ListRestaurantsViewModel, CreateReviewViewModel>()
                .ForMember(des => des.Rating, opt => opt.Ignore())
                .ForMember(des => des.Comment, opt => opt.Ignore())
                .ForMember(des => des.ReviewerName, opt => opt.Ignore())
                .ForMember(des => des.RestaurantPublicId, opt => opt.MapFrom(src => src.SelectRestaurantPublicId));

            CreateMap<CreateReviewViewModel, Review>()
                .ForMember(des => des.ReviewId, opt => opt.UseValue(Guid.NewGuid()))
                .ForMember(des => des.ReviewPublicId, opt => opt.UseValue(Guid.NewGuid()))
                .ForMember(des => des.RestaurantId, opt => opt.Ignore())
                .BeforeMap((des, src) => src.Restaurant = new Restaurant())
                .ForPath(des => des.Restaurant.RestaurantPublicId, opt => opt.MapFrom(src => src.RestaurantPublicId));
        }
    }
}
