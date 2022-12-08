using HelsinkiCityBike.DAL.Entities;
using HelsinkiCityBike.DAL.Repositories;

namespace HelsinkiCityBike.BLL.Services
{
    public class JourneyService : IJourneyService
    {
        private readonly IJourneyRepository _journeyRepository;

        public JourneyService(IJourneyRepository journeyRepository)
        {
            _journeyRepository = journeyRepository;
        }
        public async Task<List<Journey>> GetAllJourneys()
        {
            var journeys = await _journeyRepository.GetAllJourneys();
            return journeys;
        }

    }
}
