using Dapper;
using HelsinkiCityBike.DAL.Entities;

namespace HelsinkiCityBike.DAL.Repositories
{
    public class JourneyRepository : IJourneyRepository
    {
        private readonly DbContext _context;

        public JourneyRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<List<Journey>> GetAllJourneys()
        {
            return new List<Journey>();
        }

        public async Task<Journey> GetJourneyById(int id)
        {
            return new Journey();
        }
    }
}
