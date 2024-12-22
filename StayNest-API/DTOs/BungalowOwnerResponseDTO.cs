using StayNest_API.Data.Models;

namespace StayNest_API.DTOs
{
    public class BungalowOwnerResponseDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        public string Username { get; set; }
        public List<Advertisement> Advertisments { get; set; }
    }
}
