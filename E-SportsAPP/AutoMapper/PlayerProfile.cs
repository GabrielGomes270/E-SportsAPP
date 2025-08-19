using AutoMapper;
using E_SportsAPP.DTOs;
using E_SportsAPP.Models;

namespace E_SportsAPP.AutoMapper
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Player, PlayerHighlightDTO>()
                .ForMember(dest => dest.SocialLinks, opt => opt.MapFrom(src => src.SocialLinks));

        }
    }
}
