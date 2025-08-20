using E_SportsAPP.DTOs.Gear;
using E_SportsAPP.Extensions;

namespace E_SportsAPP.DTOs.Player
{
    public class PlayerResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public List<string> SocialLinks { get; set; } = new List<string>();
        public string Role { get; set; } = string.Empty;

        public int Championships { get; set; }
        public string ChampionshipsFormatted => Championships.ToReadableFormat();

        public long Followers { get; set; }
        public string FollowersFormatted => Followers.ToReadableFormat();

        public decimal Profit { get; set; }
        public string ProfitFormatted => Profit.ToReadableFormat();

        public long Visualizations { get; set; }
        public string VisualizationsFormatted => Visualizations.ToReadableFormat();

        public string ImageUrl { get; set; } = string.Empty;

        public IEnumerable<GearResponseDTO>? Gear { get; set; }
    }
}
