using AutoMapper;
using E_SportsAPP.DTOs;
using E_SportsAPP.Models;

namespace E_SportsAPP.AutoMapper
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Player, PlayerHighlightDTO>();

            CreateMap<Player, PlayerResponseDTO>();

            CreateMap<Player, PlayerDetailDTO>();

            CreateMap<Gear, GearResponseDTO>();

            CreateMap<Gear, GearDetailDTO>();
        }
    }
}
