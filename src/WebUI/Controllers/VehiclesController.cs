using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniRent.Application.Vehicles.Queries.GetVehicles;

namespace MiniRent.WebUI.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class VehiclesController : ApiControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<VehicleDto>> Get()
    {
        return await Mediator.Send(new GetVehiclesQuery());
    }
}
