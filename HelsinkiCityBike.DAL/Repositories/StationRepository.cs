using Dapper;
using HelsinkiCityBike.DAL.Entities;

namespace HelsinkiCityBike.DAL.Repositories
{
    public class StationRepository : IStationRepository
    {
        private readonly DbContext _context;

        public StationRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<List<Station>> GetAllStations()
        {
            return new List<Station>();
        }

        public async Task<Station> GetStationById(int id)
        {
            return new Station();
        }
    }
}
