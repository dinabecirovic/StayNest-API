﻿namespace StayNest_API.DTOs
{
    public class UserResponseDTO
    {
        public string Id { get; set; }  
        public string Roles { get; set; }
        public string FirstName { get; set; }    
        public string LastName { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        public string Username { get; set; }
    }
}
