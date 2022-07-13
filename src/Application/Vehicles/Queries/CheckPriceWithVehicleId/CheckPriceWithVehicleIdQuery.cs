using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MiniRent.Application.Vehicles.Queries.CheckPriceWithVehicleId;

public class CheckPriceWithVehicleIdQuery : IRequest<CheckPriceWithVehicleIdDto>
{
    public Guid Id { get; set; }
}

public class CheckPriceWithVehicleIdQueryHandler : IRequestHandler<CheckPriceWithVehicleIdQuery, CheckPriceWithVehicleIdDto>
{
    public Task<CheckPriceWithVehicleIdDto> Handle(CheckPriceWithVehicleIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
