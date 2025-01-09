namespace StayNest_API.DTOs
{
    public class LoginUserRequestDTO
    {
        public string? UserRole { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
