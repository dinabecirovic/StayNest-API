﻿namespace StayNest_API.DTOs
{
    public class RatingRequestDTO
    {
        public int UserId { get; set; }
        public int BungalowId { get; set; }
        public string Username { get; set; }
        public int Score { get; set; } 
        public string Comment { get; set; }
    }
}
