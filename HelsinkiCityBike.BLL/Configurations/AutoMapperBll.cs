using AutoMapper;
using HelsinkiCityBike.BLL.Models;
using HelsinkiCityBike.DAL.Entities;

namespace HelsinkiCityBike.BLL.Configurations
{
    public class AutoMapperBLL : Profile
    {
        public AutoMapperBLL()
        {
            CreateMap<Station, StationLongModel>();
            CreateMap<Station, StationShortModel>();

        }
    }
}
