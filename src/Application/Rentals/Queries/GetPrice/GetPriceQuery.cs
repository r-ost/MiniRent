using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MiniRent.Application.Common.Interfaces;

namespace MiniRent.Application.Rentals.Queries.GetPrice;

public class GetPriceQuery : IRequest<PriceDto>
{
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public string? Location { get; set; }
    public int RentDuration { get; set; }
}


public class GetPriceQueryHandler : IRequestHandler<GetPriceQuery, PriceDto>
{
    private readonly ICarRentalApiProxy _carRentalApiProxy;

    public GetPriceQueryHandler(ICarRentalApiProxy carRentalApiProxy)
    {
        _carRentalApiProxy = carRentalApiProxy;
    }

    public async Task<PriceDto> Handle(GetPriceQuery request, CancellationToken cancellationToken)
    {
        var priceDto = await _carRentalApiProxy.GetPriceAsync(request.Location ?? "", request.RentDuration, 
            request.Brand ?? "", request.Model ?? "");

        return priceDto;
    }
}
