namespace StayNest_API.Data.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BungalowId { get; set; }
        public int Score { get; set; }
    }
}
