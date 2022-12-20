using Dapper;
using HelsinkiCityBike.DAL.Entities;
using System.Data;

namespace HelsinkiCityBike.DAL.Repositories
{
    public class StationRepository : IStationRepository
    {
        private readonly IDbContext _context;

        public StationRepository(IDbContext context)
        {
            _context = context;
        }

        public async Task<List<Station>> GetAllStations()
        {
            using (var connection = _context.CreateConnection())
            {
                var stations = await connection.QueryAsync<Station>(
                    "dbo.Stations_SelectAll",
                    commandType: CommandType.StoredProcedure);
                return stations.ToList();
            }
        }

        public async Task<Station> GetStationById(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                var station = await connection.QueryFirstOrDefaultAsync<Station>(
                    "dbo.Stations_SelectById",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
                return station;
            }
        }

        public async Task<int> GetIdByName(string name)
        {
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QueryFirstOrDefaultAsync<int>(
                    "dbo.Stations_SelectIdByName",
                    new { Name = name },
                    commandType: CommandType.StoredProcedure);
                return id;
            }
        }

        public async Task<float> GetSumOfDistance(int id, string direction)
        {
            var query = $"SELECT SUM(dbo.Journeys.[Covered distance (m)]) " +
                        $"FROM dbo.Journeys WHERE [{direction}] = {id}";
            using (var connection = _context.CreateConnection())
            {
                var distance = await connection.QueryFirstOrDefaultAsync<float>(query);
                return distance;
            }
        }

        public async Task<List<Station>> GetTopReturnStations(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                var stations = await connection.QueryAsync<Station>(
                    "dbo.Stations_SelectTopReturnStations",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
                return stations.ToList();
            }
        }

        public async Task<List<Station>> GetTopDepartureStations(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                var stations = await connection.QueryAsync<Station>(
                    "dbo.Stations_SelectTopDepartureStations",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
                return stations.ToList();
            }
        }
    }
}
