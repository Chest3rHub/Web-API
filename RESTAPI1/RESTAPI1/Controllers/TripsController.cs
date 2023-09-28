using Microsoft.AspNetCore.Mvc;
using RESTAPI1.DTOs;
using RESTAPI1.Services;

namespace RESTAPI1.Controllers;

[Route("api/[controller]")]
[ApiController]

public class TripsController : ControllerBase
{
    private readonly ITripDbService _tripDbService;

    public TripsController(ITripDbService tripDbService)
    {
        _tripDbService = tripDbService;
    }
    [HttpGet]
    public async Task<IActionResult> GetTrips(CancellationToken cancellationToken)
    {
        IList<TripDTO> trips = await _tripDbService.GetTripsListAsync(cancellationToken);
        return Ok(trips);
    }
    
    [HttpPost("/api/trips/{idTrip}/clients")]
    public async Task<IActionResult> AssignClient(ClientTripDTO clientTrip)
    {
        try
        {
            bool result = await _tripDbService.AddClientToTripAsync(clientTrip);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok("Dodano klienta do wycieczki");
    }
}