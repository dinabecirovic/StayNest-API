using Microsoft.AspNetCore.Mvc;
using StayNest_API.Interfaces;
using StayNest_API.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace StayNest_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementService _advertisementService;

        public AdvertisementController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateAdvertisement([FromBody] AdvertisementRequestDTO request)
        {
            var advertisement = await _advertisementService.CreateAdvertisement(request);
            return Ok(advertisement);
        }

        [HttpGet("owner/{BungalowOwnerId}")]
        [Authorize]
        public async Task<IActionResult> GetOwnerAdvertisements(int ownerId)
        {
            var advertisements = await _advertisementService.GetOwnerAdvertisements(ownerId);
            return Ok(advertisements);
        }

        [HttpPut("update-price/{advertisementId}")]
        [Authorize]
        public async Task<IActionResult> UpdateAdvertisementPrice(int advertisementId, [FromBody] int newPrice)
        {
            try
            {
                await _advertisementService.UpdateAdvertisementPrice(advertisementId, newPrice);
                return Ok(new { Message = "Price updated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpDelete("{advertisementId}")]
        [Authorize]
        public async Task<IActionResult> DeleteAdvertisement(int advertisementId)
        {
            try
            {
                var bungalowOwnerId = int.Parse(User.FindFirst("id")?.Value ?? "0");

                await _advertisementService.DeleteAdvertisement(advertisementId, bungalowOwnerId);
                return Ok(new { Message = "Oglas je uspešno izbrisan." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("reservations/{BungalowOwnerId}")]
        [Authorize]
        public async Task<IActionResult> GetReservationsForOwner(int BungalowOwnerId)
        {
            var reservations = await _advertisementService.GetReservationsForOwner(BungalowOwnerId);

            return Ok(reservations);
        }
    }
}
