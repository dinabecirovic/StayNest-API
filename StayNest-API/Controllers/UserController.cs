using Microsoft.AspNetCore.Mvc;
using StayNest_API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using StayNest_API.DTOs;
using StayNest_API.Data.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace StayNest_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IMapper mapper, IUserService userService)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("valid-roles")]
        public IActionResult GetValidRoles()
        {
            var validRoles = new List<string> { "User", "BungalowOwner", "Administrator" };
            return Ok(validRoles);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequestDTO request)
        {
            var userExist = await _userService.GetUserByUsername(request.Username);
            if (userExist is not null)
            {
                return BadRequest(new { Message = "Korisničko ime već postoji." });
            }

            var emailExist = await _userService.GetUserByEmail(request.Email);
            if (emailExist is not null)
            {
                return BadRequest(new { Message = "Email već postoji." });
            }

            var user = new Users
            {
                Roles = request.Roles,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                BirthDate = request.BirthDate,
                Username = request.Username,
                Password = _userService.HashPassword(request.Password),
            };

            await _userService.RegisterUser(user);

            return Ok(new { Message = "Registracija uspešna!" });
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserRequestDTO request)
        {
            var userExist = await _userService.GetUserByUsername(request.Username);


            if (userExist is null)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Korisnik nije registrovan."
                });
            }

            if (userExist.Password != _userService.HashPassword(request.Password))
            {
                return BadRequest(new ErrorResponseDTO
                {
                    Message = "Pogrešna lozinka."
                });
            }

            var token = _userService.GenerateToken(userExist);

            // var userRole = await _userService.GetUserRole(userExist.Id);

            return Ok(new AuthResponseDTO
            {
                Token = token,
                Users = _mapper.Map<UserResponseDTO>(userExist),
                Role = userExist.Roles,

            });
        }
    }
}