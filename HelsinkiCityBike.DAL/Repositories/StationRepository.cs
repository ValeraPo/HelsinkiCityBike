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
            var query = "SELECT Name, Adress AS Address FROM [HelsinkiCityBike].[dbo].Stations";
            using (var connection = _context.CreateConnection())
            {
                var stations = await connection.QueryAsync<Station>(query);
                return stations.ToList();
            }
        }

        public async Task<Station> GetStationById(int id)
        {
            return new Station();
        }
    }
}
