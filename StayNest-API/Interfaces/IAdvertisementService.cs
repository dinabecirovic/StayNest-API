using StayNest_API.Data.Models;
using StayNest_API.DTOs;

namespace StayNest_API.Interfaces
{
    public interface IAdvertisementService
    {
        Task<List<AdvertisementResponseDTO>> GetAllAdvertisements();
        Task<AdvertisementResponseDTO> CreateAdvertisement(AdvertisementRequestDTO request);
        Task<List<AdvertisementResponseDTO>> GetOwnerAdvertisements(int BungalowOwnerId);
        Task UpdateAdvertisementPrice(int advertisementId, int newPrice);
        Task<List<Reservation>> GetReservationsForOwner(int BungalowOwnerId);
        Task DeleteAdvertisement(int advertisementId);
        Task DeleteAdvertisementByAdmin(int advertisementId);
    }
}
