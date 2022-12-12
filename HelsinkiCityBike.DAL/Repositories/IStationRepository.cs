using HelsinkiCityBike.DAL.Entities;

namespace HelsinkiCityBike.DAL.Repositories
{
    public interface IStationRepository
    {
        Task<List<Station>> GetAllStations();
        Task<Station> GetStationById(int id);
        Task<int> GetIdByName(string name);
    }
}
