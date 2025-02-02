using System.ComponentModel.DataAnnotations.Schema;

namespace StayNest_API.Data.Models
{
    public class Advertisement
    {
        public int Id { get; set; }
        public List<string> UrlPhotos { get; set; }
        public int NumbersOfRooms { get; set; }
        public int BuildingArea { get; set; }
        public string Location { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }

        [ForeignKey(nameof(BungalowOwner))]
        public int BungalowOwnerId { get; set; }
    }
}