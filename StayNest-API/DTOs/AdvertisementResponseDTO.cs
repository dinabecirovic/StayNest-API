using StayNest_API.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace StayNest_API.DTOs
{
    public class AdvertisementResponseDTO
    {
        public int Id { get; set; }
        public List<string> UrlPhotos { get; set; }
        public int NumbersOfRooms { get; set; }
        public int BuildingArea { get; set; }
        public string Location { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public int BungalowOwnerId { get; set; }
    }
}
