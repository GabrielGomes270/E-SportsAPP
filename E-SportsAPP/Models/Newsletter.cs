﻿namespace E_SportsAPP.Models
{
    public class Newsletter
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime SubscribedAt { get; set; }
    }
}
