using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniRent.Application.Rentals.Queries.GetPrice;

namespace MiniRent.WebUI.Controllers;

[Authorize]
public class RentalsController : ApiControllerBase
{
    [HttpPost]
    public async Task<PriceDto> GetPrice([FromBody] GetPriceQuery query)
    {
        return await Mediator.Send(query);
    }
}
