using HelsinkiCityBike.API.Models;
using HelsinkiCityBike.BLL.Services;
using HelsinkiCityBike.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HelsinkiCityBike.API.Controllers
{
    [ApiController]
    [Route("api/journey")]
    public class JourneyController : Controller
    {
        private readonly IJourneyService _journeyService;

        public JourneyController(IJourneyService journeyService)
        {
            _journeyService = journeyService;
        }

        // api/journey/1/20
        [HttpGet("{pageNo}/{rowsOnPage}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status504GatewayTimeout)]
        public async Task<ActionResult<List<Journey>>> GetAllJourneys(int pageNo, int rowsOnPage)
        {
            var outputs = await _journeyService.GetAllJourneys(pageNo, rowsOnPage);
            return Ok(outputs);
        }

        // api/journey/amount
        [HttpGet("amount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status504GatewayTimeout)]
        public async Task<ActionResult<int>> GetAmountOfJourneys()
        {
            var outputs = await _journeyService.GetAmountOfJourneys();
            return Ok(outputs);
        }

    }
}
