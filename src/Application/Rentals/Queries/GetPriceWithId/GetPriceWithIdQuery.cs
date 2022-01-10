using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MiniRent.Application.Common.Interfaces;

namespace MiniRent.Application.Rentals.Queries.GetPriceWithId;

public class GetPriceWithIdQuery : IRequest<PriceDto>
{
    public Guid Id { get; set; }
    public string? Location { get; set; }
    public int RentDuration { get; set; }
}


public class GetPriceWithIdQueryHandler : IRequestHandler<GetPriceWithIdQuery, PriceDto>
{
    private readonly ICarRentalApiProxy _carRentalApiProxy;

    public GetPriceWithIdQueryHandler(ICarRentalApiProxy carRentalApiProxy)
    {
        _carRentalApiProxy = carRentalApiProxy;
    }

    public async Task<PriceDto> Handle(GetPriceWithIdQuery request, CancellationToken cancellationToken)
    {
        var priceDto = await _carRentalApiProxy.GetPriceAsync(request.Location ?? "", request.RentDuration,
            request.Id);

        return priceDto;
    }
}
