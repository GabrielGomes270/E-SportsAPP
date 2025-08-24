namespace E_SportsAPP.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime PublishedAt { get; set; } = DateTime.Now;
    }
}
