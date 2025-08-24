using AutoMapper;
using E_SportsAPP.DTOs.Gear;
using E_SportsAPP.DTOs.News;
using E_SportsAPP.DTOs.Player;
using E_SportsAPP.DTOs.Product;
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

            CreateMap<CreatePlayerDTO, Player>();
            CreateMap<UpdatePlayerDTO, Player>();

            CreateMap<Gear, GearResponseDTO>();
            CreateMap<Gear, GearDetailDTO>();

            CreateMap<CreateGearDTO, Gear>();
            CreateMap<UpdateGearDTO, Gear>();

            CreateMap<Product, ProductResponseDTO>();
            CreateMap<CreateProductDTO, Product>();
            CreateMap<UpdateProductDTO, Product>();

            CreateMap<News, NewsResponseDTO>();
            CreateMap<CreateNewsDTO, News>();
        }
    }
}
