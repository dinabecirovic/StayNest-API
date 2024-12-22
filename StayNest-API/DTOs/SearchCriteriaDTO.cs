namespace StayNest_API.DTOs
{
    public class SearchCriteriaDTO
    {
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string? Location { get; set; }
        public int? MaxRooms { get; set; }
        public int? MinRooms { get; set; }
        public int? MinArea { get; set; }
        public int? MaxArea { get; set; }
    }
}
