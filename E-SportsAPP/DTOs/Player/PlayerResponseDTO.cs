using E_SportsAPP.DTOs.Gear;

namespace E_SportsAPP.DTOs.Player
{
    public class PlayerResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string SocialLinks { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int Championships { get; set; }
        public int Followers { get; set; }
        public int Profit { get; set; }
        public int Visualizations { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        public IEnumerable<GearResponseDTO>? Gear { get; set; }
    }
}
