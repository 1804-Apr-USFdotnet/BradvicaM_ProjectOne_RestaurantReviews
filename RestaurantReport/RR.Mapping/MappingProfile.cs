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
                .ForSourceMember(src => src.Id, opt => opt.Ignore());

            CreateMap<Restaurant, TopRatedRestaurantViewModel>()
                .ForSourceMember(src => src.Id, opt => opt.Ignore())
                .ForSourceMember(src => src.Website, opt => opt.Ignore())
                .ForSourceMember(src => src.PhoneNumber, opt => opt.Ignore())
                .ForSourceMember(src => src.Reviews, opt => opt.Ignore());
        }
    }
}
