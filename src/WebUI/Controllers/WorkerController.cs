using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniRent.Application.Rentals.Commands.ReturnCar;
using MiniRent.Application.Rentals.Queries.GetCurrentRentalsWorker;
using MiniRent.Application.Rentals.Queries.GetRentDetails;

namespace MiniRent.WebUI.Controllers;


[Route("api/[controller]")]
[Authorize(Policy = "Worker")]
public class WorkerController : ApiControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<CurrentRentalWorkerDto>> GetCurrentRentals()
    {
        return await Mediator.Send(new GetCurrentRentalsWorkerQuery());
    }


    [HttpPost("return")]
    public async Task<int> ReturnCar(ReturnCarCommand request)
    {
        return await Mediator.Send(request);
    }


    [HttpGet("{rentId}")]
    public async Task<RentDetailsDto> GetRentDetails(Guid rentId)
    {
        return await Mediator.Send(new GetRentDetailsQuery() { RentId = rentId});
    }
}
