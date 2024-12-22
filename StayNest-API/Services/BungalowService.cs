using StayNest_API.Data;
using StayNest_API.Data.Models;
using StayNest_API.DTOs;
using StayNest_API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace StayNest_API.Services
{
    public class BungalowService : IBungalowService
    {
        private readonly DatabaseContext databaseContext;
        private DatabaseContext _databaseContext;

        public BungalowService(DatabaseContext databaseContext) { 
            _databaseContext = databaseContext;
        }

        public async Task<List<Advertisement>> SearchAdvertisements(SearchCriteriaDTO criteria) 
        { 
            var query = _databaseContext.Advertisements.AsQueryable();

            if (criteria.MinPrice.HasValue) { 
                query = query.Where(a => a.Price >= criteria.MinPrice.Value);
            }

            if (criteria.MaxPrice.HasValue) { 
                query = query.Where(a => a.Price <= criteria.MaxPrice.Value);
            }

            if (!string.IsNullOrEmpty(criteria.Location))
                query = query.Where(a => a.Location == criteria.Location);

            if (criteria.MinRooms.HasValue) {
                query = query.Where(a => a.NumbersOfRooms >= criteria.MinRooms.Value);
            }

            if (criteria.MaxRooms.HasValue) { 
                query = query.Where(a => a.NumbersOfRooms <= criteria.MaxRooms.Value);
            }

            if (criteria.MinArea.HasValue) { 
                query = query.Where(a => a.BuildingArea >= criteria.MinArea.Value);
            }

            if (criteria.MaxArea.HasValue)
            {
                query = query.Where(a => a.BuildingArea <= criteria.MaxArea.Value);
            }

            return await query.ToListAsync();
        }

        public async Task ReserveBungalow(Reservation reservation) 
        {
            var existingReservation = await _databaseContext.Reservations
                .Where(r => r.AdvertisementId == reservation.AdvertisementId)
                .Where(r => r.StartDate < reservation.EndDate && r.EndDate > reservation.StartDate)
                .FirstOrDefaultAsync();

            if (existingReservation != null) 
            {
                throw new InvalidOperationException("Bungalov je već rezervisan");
            }

            await _databaseContext.Reservations.AddAsync(reservation);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
