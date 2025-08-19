using AutoMapper;
using E_SportsAPP.DTOs.Gear;
using E_SportsAPP.DTOs.Player;
using E_SportsAPP.Models;

namespace E_SportsAPP.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Player, PlayerHighlightDTO>();

            CreateMap<Player, PlayerResponseDTO>();
            CreateMap<Player, PlayerDetailDTO>();

            CreateMap<Gear, GearResponseDTO>();
            CreateMap<Gear, GearDetailDTO>();

            CreateMap<CreatePlayerDTO, Player>();
            CreateMap<UpdatePlayerDTO, Player>();

            CreateMap<CreateGearDTO, Gear>();
            CreateMap<UpdateGearDTO, Gear>();

        }
    }
}
