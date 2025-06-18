using Application.DTOs.FlightSearch;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightSearchController : ControllerBase
{
    private readonly FlightSearchService _flightSearchService;

    public FlightSearchController(FlightSearchService flightSearchService)
    {
        _flightSearchService = flightSearchService;
    }

    [HttpPost("roundtrip")]
    public async Task<IActionResult> RoundTripSearch([FromBody] RoundTripRequest request)
    {
        var result = await _flightSearchService.SearchRoundTripAsync(request);
        return Ok(result);
    }
}
