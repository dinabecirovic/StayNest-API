using AutoMapper;
using StayNest_API.Data.Models;
using StayNest_API.DTOs;

namespace StayNest_API.Profiles
{
    public class AdvertisementMappingProfile : Profile
    {
        public AdvertisementMappingProfile()
        {
            CreateMap<Advertisement, AdvertisementResponseDTO>();
            CreateMap<AdvertisementResponseDTO, Advertisement>();

        }
    }
}

