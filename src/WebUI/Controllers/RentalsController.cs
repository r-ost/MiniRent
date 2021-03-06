using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniRent.Application.Rentals.Commands.RentCar;
using MiniRent.Application.Rentals.Commands.ReturnCar;
using MiniRent.Application.Rentals.Queries;
using MiniRent.Application.Rentals.Queries.GetCurrentRentals;
using MiniRent.Application.Rentals.Queries.GetHistoricRentals;
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


    [HttpGet("current")]
    public async Task<IEnumerable<CurrentRentalDto>> GetCurrentRentals()
    {
        return await Mediator.Send(new GetCurrentRentalsQuery());
    }

    [HttpGet("historic")]
    public async Task<IEnumerable<HistoricRentalDto>> GetHistoricRentals()
    {
        return await Mediator.Send(new GetHistoricRentalsQuery());
    }
}
