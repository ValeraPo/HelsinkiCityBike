using HelsinkiCityBike.BLL.Exceptions;
using HelsinkiCityBike.DAL.Entities;
using HelsinkiCityBike.DAL.Repositories;

namespace HelsinkiCityBike.BLL.Services
{
    public class StationService : IStationService
    {
        private readonly IStationRepository _stationRepository;

        public StationService(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }

        public async Task<List<Station>> GetAllStations()
        {
            var stations = await _stationRepository.GetAllStations();
            return stations;
        }
        
        public async Task<Station> GetStationByName(string name)
        {
            var id = await _stationRepository.GetIdByName(name);
            if (id == default(int))
                throw new MissingEntryException($"{name} not found");

            var station = await _stationRepository.GetStationById(id);
            return station;
        }
    }
}
