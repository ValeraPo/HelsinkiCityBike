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

        public async Task<List<Journey>> GetAllJourneys()
        {
            var query = "SELECT TOP (10) " +
                        "JourneysWithNames.[Covered distance (m)] AS CoveredDistance, " +
                        "JourneysWithNames.[Duration (sec)] AS Duration, " +
                        "JourneysWithNames.Name AS DepartureStationName, " +
                        "dbo.Stations.Name AS ReturnStationName " +
                        "FROM (SELECT TOP(10) " +
                              "dbo.Journeys.*, " +
                              "dbo.Stations.Name " +
                              "FROM [HelsinkiCityBike].[dbo].[Journeys] " +
                              "LEFT JOIN dbo.Stations " +
                              "ON dbo.Journeys.[Departure station id] = dbo.Stations.ID) " +
                        "AS JourneysWithNames " +
                        "LEFT JOIN dbo.Stations " +
                        "ON JourneysWithNames.[Return station id] = dbo.Stations.ID; ";
            using (var connection = _context.CreateConnection())
            {
                var journeys = await connection.QueryAsync<Journey>(query);
                return journeys.ToList();
            }
        }

    }
}
