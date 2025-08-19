using E_SportsAPP.DTOs.Gear;

namespace E_SportsAPP.DTOs.Player
{
    public class PlayerDetailDTO : PlayerResponseDTO
    {
        public List<GearResponseDTO> Gears { get; set; } = new();
    }
}
