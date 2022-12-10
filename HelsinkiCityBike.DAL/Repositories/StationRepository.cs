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
            var queryForNameAndAddress = $"SELECT Name, Adress AS Address " +
                                         $"FROM [HelsinkiCityBike].[dbo].Stations " +
                                         $"WHERE [dbo].Stations.ID = {id}";
            var queryForCountOfDepartures = $"SELECT " +
                                            $"COUNT(dbo.Journeys.[Departure station id]) AS NumberOfJourneysStartingFrom " +
                                            $"FROM dbo.Journeys " +
                                            $"WHERE [Departure station id] = {id}";
            var queryForCountOfReturnss = $"SELECT " +
                                          $"COUNT(dbo.Journeys.[Return station id]) AS NumberOfJourneysEndingAt " +
                                          $"FROM dbo.Journeys " +
                                          $"WHERE [Return station id] = {id}";

            using (var connection = _context.CreateConnection())
            {
                var station = await connection.QueryFirstOrDefaultAsync<Station>(queryForNameAndAddress);
                station.NumberOfJourneysStartingFrom = await connection.QueryFirstOrDefaultAsync<int>(queryForCountOfDepartures);
                station.NumberOfJourneysEndingAt = await connection.QueryFirstOrDefaultAsync<int>(queryForCountOfReturnss);

                return station;
            }
        }

        public async Task<int> GetIdByName(string name)
        {
            var query = $"SELECT ID FROM [HelsinkiCityBike].[dbo].Stations WHERE [dbo].Stations.Name = '{name}'";
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QueryFirstOrDefaultAsync<int>(query);
                return id;
            }
        }
    }
}
