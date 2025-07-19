using System.ComponentModel.DataAnnotations.Schema;

namespace StayNest_API.Data.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BungalowId { get; set; }
        public string Username { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }

    }
}
