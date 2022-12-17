using AutoMapper;
using HelsinkiCityBike.BLL.Exceptions;
using HelsinkiCityBike.BLL.Models;
using HelsinkiCityBike.DAL.Repositories;

namespace HelsinkiCityBike.BLL.Services
{
    public class StationService : IStationService
    {
        private readonly IStationRepository _stationRepository;
        private readonly IMapper _automapper;

        public StationService(IStationRepository stationRepository, IMapper automapper)
        {
            _stationRepository = stationRepository;
            _automapper = automapper;
        }

        public async Task<List<StationShortModel>> GetAllStations()
        {
            var stations = _automapper
                .Map<List<StationShortModel>>(await _stationRepository.GetAllStations());
            return stations;
        }
        
        public async Task<StationLongModel> GetStationByName(string name)
        {
            var id = await _stationRepository.GetIdByName(name);
            if (id == default(int))
                throw new MissingEntryException($"Station '{name}' not found");

            var station = _automapper.Map<StationLongModel>(await _stationRepository.GetStationById(id));

            station.TopDepartureStations = _automapper
                .Map<List<StationShortModel>>(await _stationRepository.GetTopDepartureStations(id));
            station.TopReturnStations = _automapper
                .Map<List<StationShortModel>>(await _stationRepository.GetTopReturnStations(id));

            station.AvgDistanceOfJourneyStartingFrom = await _stationRepository.GetSumOfDistance(id, "Departure station id");
            station.AvgDistanceOfJourneyEndingAt = await _stationRepository.GetSumOfDistance(id, "Return station id");
            
            return station;
        }
    }
}
