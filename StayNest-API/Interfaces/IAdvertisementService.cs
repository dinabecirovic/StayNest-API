using StayNest_API.Data.Models;
using StayNest_API.DTOs;
using System.Security.Claims;

namespace StayNest_API.Interfaces
{
    public interface IAdvertisementService
    {
        Task<List<AdvertisementResponseDTO>> GetAllAdvertisements();
        Task<AdvertisementResponseDTO> CreateAdvertisement(AdvertisementRequestDTO request, ClaimsPrincipal user);
        Task<List<AdvertisementResponseDTO>> GetOwnerAdvertisements(int bungalowOwnerId);
        Task UpdateAdvertisementPrice(int advertisementId, int newPrice);
        Task<List<Reservation>> GetReservationsForAdvertisement(int advertisementId);
        Task DeleteAdvertisement(int advertisementId);
        Task DeleteAdvertisementByAdmin(int advertisementId);
    }
}
