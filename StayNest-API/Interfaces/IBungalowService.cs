using StayNest_API.Data.Models;
using StayNest_API.DTOs;

namespace StayNest_API.Interfaces
{
    public interface IBungalowService
    {
        Task<List<Advertisement>> SearchAdvertisements(SearchCriteriaDTO criteria);

        Task ReserveBungalow(Reservation reservation);

        Task AddRating(Rating rating);
        Task<List<Rating>> GetRatingsByBungalowId(int bungalowId);




    }
}

