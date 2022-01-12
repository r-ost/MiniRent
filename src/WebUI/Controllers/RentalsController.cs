using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniRent.Application.Rentals.Commands.RentCar;
using MiniRent.Application.Rentals.Commands.ReturnCar;
using MiniRent.Application.Rentals.Queries;
using MiniRent.Application.Rentals.Queries.GetPriceWithBrandAndModel;
using MiniRent.Application.Rentals.Queries.GetPriceWithId;

namespace MiniRent.WebUI.Controllers;

[Authorize]
public class RentalsController : ApiControllerBase
{
    [HttpPost("priceWithBrandAndModel")]
    public async Task<PriceDto> GetPriceWithBrandAndModel([FromBody] GetPriceWithBrandAndModelQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost("priceWithId")]
    public async Task<PriceDto> GetPriceWithId([FromBody] GetPriceWithIdQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost("rent")]
    public async Task<RentCarDto> RentCar([FromBody] RentCarCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("return/{rentId}")]
    public async Task<int> ReturnCar(Guid rentId)
    {
        return await Mediator.Send(new ReturnCarCommand() { RentId = rentId});
    }
}
