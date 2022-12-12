using Dapper;
using HelsinkiCityBike.DAL.Entities;

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
            var query = "SELECT Name, Adress AS Address FROM [HelsinkiCityBike].[dbo].Stations";
            using (var connection = _context.CreateConnection())
            {
                var stations = await connection.QueryAsync<Station>(query);
                return stations.ToList();
            }
        }

        public async Task<Station> GetStationById(int id)
        {
            var query = $"SELECT " +
                            $"Name, " +
                            $"Address, " +
                            $"NumberOfJourneysStartingFrom,	" +
                            $"NumberOfJourneysEndingAt " +
                        $"FROM " +
                              $"(SELECT " +
                                   $"ID, " +
                                   $"Name, " +
                                   $"Adress AS Address " +
                              $"FROM [HelsinkiCityBike].[dbo].Stations " +
                              $"WHERE [HelsinkiCityBike].[dbo].Stations.ID = {id}) " +
                         $"A FULL JOIN " +
                              $"(SELECT " +
                                   $"COUNT(dbo.Journeys.[Departure station id]) AS NumberOfJourneysStartingFrom, " +
                                   $"[Departure station id] AS ID " +
                              $"FROM dbo.Journeys " +
                              $"WHERE [Departure station id] = {id} " +
                              $"GROUP BY [Departure station id]) " +
                         $"B ON A.ID = B.ID FULL JOIN " +
                              $"(SELECT " +
                                   $"COUNT(dbo.Journeys.[Return station id]) AS NumberOfJourneysEndingAt, " +
                                   $"[Return station id] AS ID	" +
                              $"FROM dbo.Journeys " +
                              $"WHERE [Return station id] = {id}" +
                              $"GROUP BY [Return station id]) " +
                         $"C ON B.ID = C.ID";
            using (var connection = _context.CreateConnection())
            {
                var station = await connection.QueryFirstOrDefaultAsync<Station>(query);
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
