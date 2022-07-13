using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniRent.Application.Vehicles.Queries.GetVehicles;

namespace MiniRent.Application.Common.Interfaces;

public interface IVehicleService
{
    Task<IEnumerable<VehicleDto>> GetVehiclesAsync();
}
