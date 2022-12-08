
namespace HelsinkiCityBike.BLL.Services
{
    public interface IJourneyService
    {
        Task GetAllJourneys();
        Task GetJourneyById(int id);
    }
}