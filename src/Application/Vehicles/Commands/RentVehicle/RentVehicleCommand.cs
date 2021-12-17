using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MiniRent.Application.Vehicles.Commands.RentVehicle;

public class RentVehicleCommand : IRequest<RentVehicleDto>
{
    public Guid QuoteId { get; set; }
}


public class RentVehicleCommandHandler : IRequestHandler<RentVehicleCommand, RentVehicleDto>
{
    public Task<RentVehicleDto> Handle(RentVehicleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}