using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MiniRent.Application.Common.Interfaces;

namespace MiniRent.Application.Vehicles.Queries.GetVehicles;

public class GetVehiclesQuery : IRequest<IEnumerable<VehicleDto>>
{

}

public class GetVehiclesQueryHandler : IRequestHandler<GetVehiclesQuery, IEnumerable<VehicleDto>>
{
    private readonly IVehicleService _vehicleService;

    public GetVehiclesQueryHandler(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    public async Task<IEnumerable<VehicleDto>> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
    {
        return await _vehicleService.GetVehiclesAsync();
    }
}