using AutoMapper;
using HelsinkiCityBike.API.Models;
using HelsinkiCityBike.DAL.Entities;

namespace HelsinkiCityBike.API.Configuration
{
    public class AutoMapperApi : Profile
    {
        public AutoMapperApi()
        {
            CreateMap<Station, StationShortResponseModel>();
        }
    }
}
