using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StayNest_API.DTOs;
using StayNest_API.Interfaces;
using StayNest_API.Data.Models;
using StayNest_API.Data;
using System.Security.Claims;

namespace StayNest_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]     
    public class BungalowController : ControllerBase
    {
        private readonly IBungalowService _bungalowService;
        private readonly DatabaseContext _databaseContext;
        public BungalowController(IBungalowService bungalowService, DatabaseContext databaseContext)
        { 
            _bungalowService = bungalowService;
            _databaseContext = databaseContext;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBungalows([FromQuery] SearchCriteriaDTO criteria) 
        { 
            var results = await _bungalowService.SearchAdvertisements(criteria);
            return Ok(results);
        }

        [HttpPost("reserve")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ReserveBungalow([FromBody] ReservationRequestDTO request)
        {
            try
            {
                // 1. Izvuci ID korisnika iz tokena
                var userIdClaim = User.FindFirst("id") ?? User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                    return Unauthorized(new { Message = "Nevažeći token." });

                int userId = int.Parse(userIdClaim.Value);

                // 2. Nađi korisnika u bazi
                var user = await _databaseContext.Users.FindAsync(userId);
                if (user == null)
                    return NotFound(new { Message = "Korisnik nije pronađen." });

                // 3. Kreiraj rezervaciju sa podacima iz baze
                var reservation = new Reservation
                {
                    AdvertisementId = request.AdvertisementId,
                    UserId = userId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                };

                await _bungalowService.ReserveBungalow(reservation);

                return Ok(new { Message = "Uspešno ste rezervisali bungalov." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        [HttpPost("rate")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RateBungalow([FromBody] RatingRequestDTO request)
        {
            var rating = new Rating
            {
                UserId = request.UserId,
                BungalowId = request.BungalowId,
                Username = request.Username,
                Score = request.Score,
                Comment = request.Comment,
            };

            await _bungalowService.AddRating(rating);
            return Ok(new { Message = "Hvala na povratnoj informaciji." });
        }

        [HttpGet("ratings/{bungalowId}")]
        public async Task<IActionResult> GetRatings(int bungalowId)
        {
            var ratings = await _bungalowService.GetRatingsByBungalowId(bungalowId);

            var response = ratings.Select(r => new RatingResponseDto
            {
                Id = r.Id,
                UserId = r.UserId,
                BungalowId = r.BungalowId,
                Score = r.Score,
                Comment = r.Comment,
                Username = r.Username
            }).ToList();

            return Ok(response);
        }




    }
}
