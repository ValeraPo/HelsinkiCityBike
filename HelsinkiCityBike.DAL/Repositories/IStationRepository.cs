using HelsinkiCityBike.DAL.Entities;

namespace HelsinkiCityBike.DAL.Repositories
{
    public interface IStationRepository
    {
        Task<List<Station>> GetAllStations();
        Task<Station> GetStationById(int id);
        Task<int> GetIdByName(string name);
        Task<float> GetSumOfDistance(int id, string direction);
        Task<List<Station>> GetTopReturnStations(int id);
        Task<List<Station>>  GetTopDepartureStations(int id);

    }
}
