
namespace HelsinkiCityBike.BLL.Services
{
    public interface IStationService
    {
        Task GetAllStations();
        Task GetStationById(int id);
    }
}