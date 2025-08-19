namespace E_SportsAPP.DTOs.Gear
{
    public class GearDetailDTO
    {
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public int PlayerId { get; set; }
    }
}
