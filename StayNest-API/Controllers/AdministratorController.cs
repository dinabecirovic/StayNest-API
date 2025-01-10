using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StayNest_API.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class AdministratorController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAdvertisementService _advertisementService;

    public AdministratorController(IUserService userService, IAdvertisementService advertisementService)
    {
        _userService = userService;
        _advertisementService = advertisementService;
    }

    [HttpGet("debug-claims")]
    [Authorize]
    public IActionResult DebugClaims()
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
        return Ok(claims);
    }


    [HttpGet("users")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> GetAllUser()
    { 
        var users = await _userService.GetAllUserAsync();
        return Ok(users);
    }

    [HttpDelete("users/{userId}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        try
        {
            await _userService.DeleteUser(userId);
            return Ok(new { Message = "Korisnik je uspešno izbrisan.." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

    [HttpDelete("advertisements/{advertisementId}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteAdvertisement(int advertisementId)
    {
        try
        {
            await _advertisementService.DeleteAdvertisementByAdmin(advertisementId);
            return Ok(new { Message = "Oglas je uspešno izbrisan." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }
}
