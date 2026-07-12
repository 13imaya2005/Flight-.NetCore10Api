using AutoMapper;
using Flight_API.Dto;
using Flight_API.Entities;

namespace FlightAPI.Mapper
{
    public class FlightMasterProfile : Profile
    {
        public FlightMasterProfile()
        {
            CreateMap<FlightMaster, FlightDto>().ReverseMap();
        }
    }
}