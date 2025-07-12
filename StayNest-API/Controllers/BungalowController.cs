using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StayNest_API.DTOs;
using StayNest_API.Interfaces;
using StayNest_API.Data.Models;

namespace StayNest_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]     
    public class BungalowController : ControllerBase
    {
        private readonly IBungalowService _bungalowService;
        public BungalowController(IBungalowService bungalowService)
        { 
            _bungalowService = bungalowService;
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
                var reservation = new Reservation
                {
                    AdvertisementId = request.AdvertisementId,
                    UserId = request.UserId,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
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
            return Ok(ratings);
        }



    }
}
