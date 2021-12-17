using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRent.Application.Vehicles.Queries.GetVehicles;

public class VehicleDto
{
    public Guid Id { get; init; }

    public string? BrandName { get; init; }

    public string? ModelName { get; init; }

    public int Year { get; init; }

    public int EnginePower { get; init; }

    public string? EnginePowerType { get; init; }

    public int Capacity { get; init; }

    public string? Description { get; init; }
}
