using StayNest_API.Data.Models;

namespace StayNest_API.DTOs
{
    public class BungalowOwnerRequestDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Advertisement> Advertisments { get; set; }
    }
}
