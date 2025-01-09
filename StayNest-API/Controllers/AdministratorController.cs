using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StayNest_API.Interfaces;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")]
public class AdministratorController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAdvertisementService _advertisementService;

    public AdministratorController(IUserService userService, IAdvertisementService advertisementService)
    {
        _userService = userService;
        _advertisementService = advertisementService;
    }

    [HttpDelete("users/{userId}")]
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
