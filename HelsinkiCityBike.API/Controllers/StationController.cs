using AutoMapper;
using HelsinkiCityBike.API.Models;
using HelsinkiCityBike.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelsinkiCityBike.API.Controllers
{
    [ApiController]
    [Route("api/station")]
    public class StationController : Controller
    {
        private readonly IStationService _stationService;
        private readonly IMapper _automapper;

        public StationController(IStationService stationService, IMapper automapper)
        {
            _stationService = stationService;
            _automapper = automapper;
        }

        // api/station
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<StationShortResponse>>> GetAllStations()
        {
            var outputs = _automapper.Map<List<StationShortResponse>>(await _stationService.GetAllStations());
            return Ok(outputs);
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
