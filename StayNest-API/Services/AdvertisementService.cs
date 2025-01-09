using StayNest_API.Data;
using StayNest_API.Data.Models;
using StayNest_API.DTOs;
using StayNest_API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StayNest_API.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly DatabaseContext _databaseContext;

        public AdvertisementService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<AdvertisementResponseDTO> CreateAdvertisement(AdvertisementRequestDTO request)
        {
            var advertisement = new Advertisement
            {
                UrlPhoto = request.UrlPhoto,
                NumbersOfRooms = request.NumbersOfRooms,
                BuildingArea = request.BuildingArea,
                Location = request.Location,
                Price = request.Price,
                Description = request.Description,
                BungalowOwnerId = request.BungalowOwnerId,
                IsAvailable = true
            };

            await _databaseContext.Advertisements.AddAsync(advertisement);
            await _databaseContext.SaveChangesAsync();

            return new AdvertisementResponseDTO
            {
                Id = advertisement.Id,
                UrlPhoto = request.UrlPhoto,
                NumbersOfRooms = request.NumbersOfRooms,
                BuildingArea = request.BuildingArea,
                Location = request.Location,
                Price = request.Price,
                Description = request.Description,
                BungalowOwnerId = request.BungalowOwnerId,
                IsAvailable = true
            };
        }

        public async Task<List<AdvertisementResponseDTO>> GetOwnerAdvertisements(int BungalowOwnerId)
        {
            var advertisements = await _databaseContext.Advertisements
                .Where(a => a.BungalowOwnerId == BungalowOwnerId)
                .Select(a => new AdvertisementResponseDTO
                {
                    Id = a.Id,
                    UrlPhoto = a.UrlPhoto,
                    NumbersOfRooms = a.NumbersOfRooms,
                    BuildingArea = a.BuildingArea,
                    Location = a.Location,
                    Price = a.Price,
                    Description = a.Description,
                    BungalowOwnerId = a.BungalowOwnerId,
                    IsAvailable = true
                })
                .ToListAsync();

            return advertisements;
        }

        public async Task UpdateAdvertisementPrice(int advertisementId, int newPrice)
        {
            var advertisement = await _databaseContext.Advertisements.FindAsync(advertisementId);

            if (advertisement == null)
                throw new KeyNotFoundException("Advertisement not found.");

            advertisement.Price = newPrice;
            await _databaseContext.SaveChangesAsync();
        }

        public async Task DeleteAdvertisement(int advertisementId, int bungalowOwnerId)
        {
            var advertisement = await _databaseContext.Advertisements
                .FirstOrDefaultAsync(a => a.Id == advertisementId && a.BungalowOwnerId == bungalowOwnerId);

            if (advertisement == null)
                throw new KeyNotFoundException("Oglas nije pronađen.");

            _databaseContext.Advertisements.Remove(advertisement);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<List<Reservation>> GetReservationsForOwner(int BungalowOwnerId)
        {
            return await _databaseContext.Reservations
                .Include(r => r.Advertisement)
                .Where(r => r.Advertisement.BungalowOwnerId == BungalowOwnerId)
                .ToListAsync();
        }

        public async Task DeleteAdvertisementByAdmin(int advertisementId)
        {
            var advertisement = await _databaseContext.Advertisements.FindAsync(advertisementId);

            if (advertisement == null)
                throw new KeyNotFoundException("Oglas nije pronađen.");

            _databaseContext.Advertisements.Remove(advertisement);
            await _databaseContext.SaveChangesAsync();
        }

    }
}
