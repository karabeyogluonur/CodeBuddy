using AutoMapper;
using CB.Application.Models.Admin.Features;
using CB.Application.Models.User.Authentication;
using CB.Domain.Entities.Features;
using CB.Domain.Entities.Membership;

namespace CB.Application.Mappers.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region User

            CreateMap<RegisterViewModel, AppUser>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ReverseMap();

            #endregion


            #region Admin

            CreateMap<TalentCreateModel, Talent>().ReverseMap();
            CreateMap<TalentListModel,Talent>().ReverseMap();
            CreateMap<TalentUpdateModel, Talent>()
                .ForMember(dest=>dest.Id, opt=>opt.Ignore())
                .ReverseMap();

            #endregion

        }

    }
}
