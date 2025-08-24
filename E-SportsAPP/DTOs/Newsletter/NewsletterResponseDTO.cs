namespace E_SportsAPP.DTOs.Newsletter
{
    public class NewsletterResponseDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime SubscribedAt { get; set; }
    }
}
