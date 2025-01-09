namespace StayNest_API.DTOs
{
    public class RegisterUserRequestDTO
    {
        public int Id { get; set; }
        public string UserRole { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
