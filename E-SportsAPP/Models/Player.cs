namespace E_SportsAPP.Models
{
    public class Player
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
        public bool IsHighlighted { get; set; } = false; 


        public ICollection<Gear> Gears { get; set; } = new List<Gear>();
    }
}
