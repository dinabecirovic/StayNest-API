using Microsoft.AspNetCore.Mvc;
using StayNest_API.DTOs;
using StayNest_API.Interfaces;

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
    }
}
