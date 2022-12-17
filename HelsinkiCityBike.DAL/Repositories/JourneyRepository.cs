using Dapper;
using HelsinkiCityBike.DAL.Entities;

namespace HelsinkiCityBike.DAL.Repositories
{
    public class JourneyRepository : IJourneyRepository
    {
        private readonly IDbContext _context;

        public JourneyRepository(IDbContext context)
        {
            _context = context;
        }

        public async Task<List<Journey>> GetAllJourneys(int rowStart, int rows)
        {
            var query = $"SELECT  " +
                        $"JourneysWithNames.[Covered distance (m)] AS CoveredDistance, " +
                        $"JourneysWithNames.[Duration (sec)] AS Duration, " +
                        $"JourneysWithNames.Name AS DepartureStationName, " +
                        $"JourneysWithNames.Id, " +
                        $"dbo.Stations.Name AS ReturnStationName " +
                        $"FROM (SELECT  " +
                              $"dbo.Journeys.*, " +
                              $"dbo.Stations.Name " +
                              $"FROM [HelsinkiCityBike].[dbo].[Journeys] " +
                              $"LEFT JOIN dbo.Stations " +
                              $"ON dbo.Journeys.[Departure station id] = dbo.Stations.ID) " +
                        $"AS JourneysWithNames " +
                        $"LEFT JOIN dbo.Stations " +
                        $"ON JourneysWithNames.[Return station id] = dbo.Stations.ID " +
                        $"ORDER BY JourneysWithNames.Id OFFSET {rowStart} ROWS FETCH NEXT {rows} ROWS ONLY; ";
            using (var connection = _context.CreateConnection())
            {
                var journeys = await connection.QueryAsync<Journey>(query);
                return journeys.ToList();
            }
        }

        public async Task<int> GetAmountOfJourneys()
        {
            var query = $"SELECT COUNT(*) FROM [HelsinkiCityBike].[dbo].[Journeys]";

            using (var connection = _context.CreateConnection())
            {
                var amount = await connection.QueryFirstOrDefaultAsync<int>(query);
                return amount;
            }
        }
    }
}
