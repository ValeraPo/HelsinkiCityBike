using AutoMapper;
using HelsinkiCityBike.API.Models;
using HelsinkiCityBike.BLL.Services;
using HelsinkiCityBike.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelsinkiCityBike.API.Controllers
{
    [ApiController]
    [Route("api/station")]
    public class StationController : Controller
    {
        private readonly IStationService _stationService;

        public StationController(IStationService stationService)
        {
            _stationService = stationService;
        }

        // api/station
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status504GatewayTimeout)]
        public async Task<ActionResult<List<StationShortModel>>> GetAllStations()
        {
            var outputs = await _stationService.GetAllStations();
            return Ok(outputs);
        }

        // api/station/Kaivopuisto
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status504GatewayTimeout)]
        public async Task<ActionResult<StationLongModel>> GetStationByName(string name)
        {
            var output = await _stationService.GetStationByName(name);
            return Ok(output);
        }

    }
}
