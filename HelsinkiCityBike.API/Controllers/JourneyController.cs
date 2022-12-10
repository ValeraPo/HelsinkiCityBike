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

        // api/journey
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Journey>>> GetAllJourneys()
        {
            var outputs = await _journeyService.GetAllJourneys();
            return Ok(outputs);
        }

    }
}
