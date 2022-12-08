using HelsinkiCityBike.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelsinkiCityBike.DAL.Repositories
{
    public interface IJourneyRepository
    {
        Task<List<Journey>> GetAllJourneys();
    }
}
