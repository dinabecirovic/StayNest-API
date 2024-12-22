namespace StayNest_API.DTOs
{
    public class ReservationRequestDTO
    {
        public int UserId { get; set; }
        public int AdverisementId { get; set; }
        public string AdvertisementId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
