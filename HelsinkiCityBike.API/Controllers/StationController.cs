using Microsoft.AspNetCore.Mvc;

namespace HelsinkiCityBike.API.Controllers
{
    [ApiController]
    [Route("api/station")]
    public class StationController : Controller
    {
        // api/station
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllStations()
        {
            return Ok();
        }

        // api/station(42)
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetStationById(int id)
        {
            return Ok($"{id}");
        }

    }
}
