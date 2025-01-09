using System.ComponentModel.DataAnnotations.Schema;

namespace StayNest_API.Data.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? UserId { get; set; }
        public int? BungalowOwnerId { get; set; }
        public int? AdministratorId { get; set; }
    }
}
