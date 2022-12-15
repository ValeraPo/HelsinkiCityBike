using HelsinkiCityBike.DAL.Entities;

namespace HelsinkiCityBike.DAL.Repositories
{
    public interface IJourneyRepository
    {
        Task<List<Journey>> GetAllJourneys(int rowStart, int rows);
        Task<int> GetAmountOfJourneys();
    }
}
