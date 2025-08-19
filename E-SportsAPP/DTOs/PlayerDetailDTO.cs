namespace E_SportsAPP.DTOs
{
    public class PlayerDetailDTO : PlayerResponseDTO
    {
        public List<GearResponseDTO> Gears { get; set; } = new();
    }
}
