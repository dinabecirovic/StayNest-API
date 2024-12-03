using Microsoft.AspNetCore.Mvc;
using StayNest_API.Interfaces;
using AutoMapper;


namespace StayNest_API.Controllers
{
    public class UserController :ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;   
             _mapper = mapper;
        }
    }
}
