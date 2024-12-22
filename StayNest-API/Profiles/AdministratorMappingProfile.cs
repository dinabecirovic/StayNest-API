using AutoMapper;
using StayNest_API.Data.Models;
using StayNest_API.DTOs;

namespace StayNest_API.Profiles
{
    public class AdministratorMappingProfile : Profile
    {
        public AdministratorMappingProfile() {
            CreateMap<Administrator, AdministratorResponseDTO>();
        }
    }
}
