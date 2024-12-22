using AutoMapper;
using StayNest_API.Data.Models;
using StayNest_API.DTOs;

namespace StayNest_API.Profiles
{
    public class BungalowOwnerMappingProfile : Profile
    {
        public BungalowOwnerMappingProfile() {
            CreateMap<BungalowOwner, BungalowOwnerResponseDTO>();
            CreateMap<BungalowOwnerRequestDTO, BungalowOwner>();
        }
    }
}
