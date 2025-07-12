namespace StayNest_API.DTOs
{
    public class ReservationRequestDTO
    {
        public int UserId { get; set; }
        public int AdvertisementId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
