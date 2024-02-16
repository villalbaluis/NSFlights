using Microsoft.AspNetCore.Mvc;
using NSFlightsBusiness;
using NSFlightsDataAccess;

[ApiController]
[Route("api/[controller]")]
public class JourneyController : ControllerBase
{
    private readonly JourneyService _journeyService;
    private readonly IJourneyRepository _journeyRepository;

    public JourneyController(IJourneyRepository journeyRepository, JourneyService journeyService)
    {
        _journeyRepository = journeyRepository ?? throw new ArgumentNullException(nameof(journeyRepository));
        _journeyService = journeyService;
    }

    [HttpGet("getJourney")]
    public async Task<IActionResult> GetJourney(string origin, string destination)
    {
        try
        {
            var journey = await _journeyService.GetJourneyAsync(origin.ToUpper(), destination.ToUpper(), "https://recruiting-api.newshore.es/api/flights/2");
            if(journey != null)
            {
                await _journeyRepository.SaveJourneyAsync(journey);
                return Ok(journey);
            }
            return StatusCode(503, $"There's no match information about the journey. Try again with other places.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error, error: {ex.Message}, please contact with ST.");
        }
    }


}
