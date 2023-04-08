using AutoMapper;
using CB.Application.Models.User.Authentication;
using CB.Domain.Entities.Membership;

namespace CB.Application.Mappers.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterViewModel, AppUser>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ReverseMap();

        }
        
    }
}
