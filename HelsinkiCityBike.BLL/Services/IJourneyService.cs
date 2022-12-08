
using HelsinkiCityBike.DAL.Entities;

namespace HelsinkiCityBike.BLL.Services
{
    public interface IJourneyService
    {
        Task<List<Journey>> GetAllJourneys();
    }
}