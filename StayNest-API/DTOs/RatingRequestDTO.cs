namespace StayNest_API.DTOs
{
    public class RatingRequestDTO
    {
        public int UserId { get; set; }
        public int BungalowId { get; set; }
        public int Score { get; set; } 
    }
}
