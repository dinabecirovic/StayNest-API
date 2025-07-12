using StayNest_API.Data;
using StayNest_API.Data.Models;
using StayNest_API.DTOs;
using StayNest_API.Interfaces;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Security.Claims;

namespace StayNest_API.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly Cloudinary _cloudinary;

        public AdvertisementService(DatabaseContext databaseContext, Cloudinary cloudinary)
        {
            _databaseContext = databaseContext;
            _cloudinary = cloudinary;
        }

        public async Task<List<AdvertisementResponseDTO>> GetAllAdvertisements()
        {
            var advertisements = await _databaseContext.Advertisements
                .Select(a => new AdvertisementResponseDTO
                {
                    Id = a.Id,
                    UrlPhotos = a.UrlPhotos,
                    NumbersOfRooms = a.NumbersOfRooms,
                    BuildingArea = a.BuildingArea,
                    Location = a.Location,
                    Price = a.Price,
                    Description = a.Description,
                    BungalowOwnerId = a.BungalowOwnerId,
                    IsAvailable = a.IsAvailable
                })
                .ToListAsync();

            return advertisements;
        }

        private async Task<string> UploadImageToCloudinary(IFormFile file)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                UploadPreset = "ml_default" // Koristi svoj preset ovde
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            if (uploadResult?.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.AbsoluteUri; // Vraćamo sigurni URL slike
            }

            return null;
        }

        public async Task<AdvertisementResponseDTO> CreateAdvertisement(AdvertisementRequestDTO request, ClaimsPrincipal user)
        {
            var uploadedUrls = new List<string>();

            // Obradjujemo svaku sliku i uploadujemo je na Cloudinary
            foreach (var file in request.Photos)
            {
                var uploadResult = await UploadImageToCloudinary(file);
                if (uploadResult != null)
                {
                    uploadedUrls.Add(uploadResult);
                }
            }

            var bungalowOwnerId = int.Parse(user.FindFirst("id")?.Value ?? "0");
            Console.WriteLine($"BungalowOwnerId iz tokena: {bungalowOwnerId}");

            // Kreiramo oglas
            var advertisement = new Advertisement
            {
                UrlPhotos = uploadedUrls,  
                NumbersOfRooms = request.NumbersOfRooms,
                BuildingArea = request.BuildingArea,
                Location = request.Location,
                Price = request.Price,
                Description = request.Description,
                BungalowOwnerId = bungalowOwnerId,
                IsAvailable = true
            };

            await _databaseContext.Advertisements.AddAsync(advertisement);
            await _databaseContext.SaveChangesAsync();

            return new AdvertisementResponseDTO
            {
                Id = advertisement.Id,
                UrlPhotos = uploadedUrls,  
                NumbersOfRooms = request.NumbersOfRooms,
                BuildingArea = request.BuildingArea,
                Location = request.Location,
                Price = request.Price,
                Description = request.Description,
                BungalowOwnerId = bungalowOwnerId,
                IsAvailable = true
            };
        }
        public async Task<List<AdvertisementResponseDTO>> GetOwnerAdvertisements(int bungalowOwnerId)
        {
            var advertisements = await _databaseContext.Advertisements
                .Where(a => a.BungalowOwnerId == bungalowOwnerId)
                .Select(a => new AdvertisementResponseDTO
                {
                    Id = a.Id,
                    UrlPhotos = a.UrlPhotos,  
                    NumbersOfRooms = a.NumbersOfRooms,
                    BuildingArea = a.BuildingArea,
                    Location = a.Location,
                    Price = a.Price,
                    Description = a.Description,
                    BungalowOwnerId = a.BungalowOwnerId,
                    IsAvailable = a.IsAvailable
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

        public async Task DeleteAdvertisement(int advertisementId)
        {
            var advertisement = await _databaseContext.Advertisements
                .FirstOrDefaultAsync(a => a.Id == advertisementId);

            if (advertisement == null)
                throw new KeyNotFoundException("Oglas nije pronađen.");

            _databaseContext.Advertisements.Remove(advertisement);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<List<Reservation>> GetReservationsForAdvertisement(int advertisementId)
        {
            return await _databaseContext.Reservations
                .Include(r => r.Advertisement)
                .Where(r => r.Advertisement.Id == advertisementId)
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
