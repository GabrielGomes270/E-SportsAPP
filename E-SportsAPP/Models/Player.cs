namespace E_SportsAPP.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public List<string> SocialLinks { get; set; } = new List<string>();
        public string Contact { get; set; } = string.Empty;
        public List<string> Games { get; set; } = new List<string>();
        public string Role { get; set; } = string.Empty;
        public int Championships { get; set; }
        public int Followers { get; set; }
        public int Profit { get; set; } 
        public int Visualizations { get; set; }
        public bool IsHighlighted { get; set; } = false;
        public string ImageUrl { get; set; } = string.Empty;


        public ICollection<Gear> Gears { get; set; } = new List<Gear>();
    }
}
