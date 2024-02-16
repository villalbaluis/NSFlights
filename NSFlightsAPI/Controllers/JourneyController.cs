using Microsoft.AspNetCore.Mvc;
using NSFlightsBusiness;
using NSFlightsDataAccess;

[ApiController]
[Route("api/[controller]")]
public class JourneyController : ControllerBase
{
    private readonly JourneyService _journeyService;

    public JourneyController(JourneyService journeyService)
    {
        _journeyService = journeyService;
    }

    [HttpGet("getJourney")]
    public async Task<IActionResult> GetJourney(string origin, string destination)
    {
        try
        {
            var journey = await _journeyService.GetJourneyAsync(origin, destination, "https://recruiting-api.newshore.es/api/flights/2");
            return Ok(journey);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }


}
