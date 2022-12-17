using HelsinkiCityBike.BLL.Models;
using HelsinkiCityBike.DAL.Entities;

namespace HelsinkiCityBike.BLL.Services
{
    public interface IStationService
    {
        Task<List<StationShortModel>> GetAllStations();
        Task<StationLongModel> GetStationByName(string name);
    }
}