using Dapper;
using HelsinkiCityBike.DAL.Entities;
using System.Data;

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
            using (var connection = _context.CreateConnection())
            {
                var journeys = await connection.QueryAsync<Journey>(
                    "dbo.Journeys_SelectAll",
                    new { RowStart = rowStart, Rows = rows },
                    commandType: CommandType.StoredProcedure);
                return journeys.ToList();
            }
        }

        public async Task<int> GetAmountOfJourneys()
        {

            using (var connection = _context.CreateConnection())
            {
                var amount = await connection.QueryFirstOrDefaultAsync<int>(
                    "dbo.Journeys_GetAmount",
                    commandType: CommandType.StoredProcedure);
                return amount;
            }
        }
    }
}
