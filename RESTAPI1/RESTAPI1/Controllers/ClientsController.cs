using Microsoft.AspNetCore.Mvc;
using RESTAPI1.Services;

namespace RESTAPI1.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ClientsController : ControllerBase
{
    private readonly ITripDbService _tripDbService;

    public ClientsController(ITripDbService tripDbService)
    {
        _tripDbService = tripDbService;
    }
    
    [HttpDelete("/api/clients/{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        bool result = await _tripDbService.DeleteClientAsync(idClient);

        if (!result)
        {
            return BadRequest("Operacja nie udana- ma wycieczki lub juz nie istnieje");
        }

        return Ok("Usunieto klienta.");
    }
}