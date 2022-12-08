using Microsoft.AspNetCore.Mvc;

namespace HelsinkiCityBike.API.Controllers
{
    [ApiController]
    [Route("api/journey")]
    public class JourneyController : Controller
    {
        // api/journey
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllJourneys()
        {
            return Ok();
        }

        // api/journey(42)
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetJourneyById(int id)
        {
            return Ok($"{id}");
        }

    }
}
