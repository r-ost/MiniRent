using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MiniRent.Application.Common.Interfaces;

namespace MiniRent.Application.Rentals.Queries.GetPriceWithBrandAndModel;

public class GetPriceWithBrandAndModelQuery : IRequest<PriceDto>
{
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public string? Location { get; set; }
    public int RentDuration { get; set; }
    public string? Company {get; set;} 
}


public class GetPriceWithBrandAndModelQueryHandler : IRequestHandler<GetPriceWithBrandAndModelQuery, PriceDto>
{
    private readonly ICarRentalApiProxy _carRentalApiProxy;

    public GetPriceWithBrandAndModelQueryHandler(ICarRentalApiProxy carRentalApiProxy)
    {
        _carRentalApiProxy = carRentalApiProxy;
    }

    public async Task<PriceDto> Handle(GetPriceWithBrandAndModelQuery request, CancellationToken cancellationToken)
    {
        var priceDto = await _carRentalApiProxy.GetPriceAsync(request.Location ?? "", request.RentDuration,
            request.Brand ?? "", request.Model ?? "", request.Company ?? "");

        return priceDto;
    }
}
