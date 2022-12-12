using System.Data;

namespace HelsinkiCityBike.DAL
{
    public interface IDbContext
    {
        IDbConnection CreateConnection();
    }
}