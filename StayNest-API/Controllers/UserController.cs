using Microsoft.AspNetCore.Mvc;
using StayNest_API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using StayNest_API.DTOs;
using StayNest_API.Data.Models;

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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUser() =>
            Ok(_mapper.Map<List<UserResponseDTO>>(await _userService.GetAllUserAsync()));

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequestDTO request)
        {
            var userExist = await _userService.GetUserByUsername(request.Username);

            if (userExist is not null)
            {
                return BadRequest(new ErrorResponseDTO
                {
                    Message = "Korisnik već postoji."
                });
            }

            var user = _mapper.Map<Users>(request);
            user.Password = _userService.HashPassword(request.Password);

            await _userService.RegisterUser(user);

            await _userService.CreateRole(new UserRole
            {
                Name = "User",
                UserId = user.Id
            });

            var token = _userService.GenerateToken(user);

            return Ok(new AuthResponseDTO
            {
                Token = token,
                Users = _mapper.Map<UserResponseDTO>(user)
            });
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

            return Ok(new AuthResponseDTO
            {
                Token = token,
                Users = _mapper.Map<UserResponseDTO>(userExist)
            });
        }
    }
}
