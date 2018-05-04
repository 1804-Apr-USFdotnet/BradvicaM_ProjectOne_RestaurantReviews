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
        }
    }
}
