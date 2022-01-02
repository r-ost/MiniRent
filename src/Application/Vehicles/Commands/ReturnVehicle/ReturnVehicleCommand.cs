using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MiniRent.Application.Vehicles.Commands.ReturnVehicle;

public class ReturnVehicleCommand : IRequest
{
    public Guid RentId { get; set; }
}

public class ReturnVehicleCommandHandler : IRequestHandler<ReturnVehicleCommand>
{
    public Task<Unit> Handle(ReturnVehicleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
