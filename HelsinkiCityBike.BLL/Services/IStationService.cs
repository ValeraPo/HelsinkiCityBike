using HelsinkiCityBike.DAL.Entities;

namespace HelsinkiCityBike.BLL.Services
{
    public interface IStationService
    {
        Task<List<Station>> GetAllStations();
        Task GetStationById(int id);
    }
}