namespace StayNest_API.DTOs
{
    public class RatingResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BungalowId { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
    }

}
