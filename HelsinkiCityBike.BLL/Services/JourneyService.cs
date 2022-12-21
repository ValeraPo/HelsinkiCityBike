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
        public async Task<List<Journey>> GetAllJourneys(int pageNo, int rowsOnPage)
        {
            var rowStart = (pageNo - 1) * rowsOnPage;
            var amountOfJourneys = await _journeyRepository.GetAmountOfJourneys();
            if (rowStart > amountOfJourneys)
                throw new ArgumentOutOfRangeException("The page you are trying to access is out of bounds.");
            var journeys = await _journeyRepository.GetAllJourneys(rowStart, rowsOnPage);
            return journeys;
        }
    }
}
