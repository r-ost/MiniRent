using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniRent.Application.Common.Interfaces;
using MiniRent.Application.Vehicles.Queries.GetVehicles;
using MiniRent.Infrastructure.CarRentalApi;

namespace MiniRent.Infrastructure.Services;

public class VehicleService : IVehicleService
{
    private readonly ICarRentalApi _carRentalApi;

    public VehicleService(ICarRentalApi carRentalApi)
    {
        _carRentalApi = carRentalApi;
    }

    public async Task<IEnumerable<VehicleDto>> GetVehiclesAsync()
    {
        var response = await _carRentalApi.GetVehiclesAsync();

        var result = new List<VehicleDto>(response.VehiclesCount);

        foreach (var vehicle in response.Vehicles)
        {
            result.Add(new VehicleDto
            {
                BrandName = vehicle.BrandName,
                Capacity = vehicle.Capacity,
                Description = vehicle.Description,
                EnginePower = vehicle.EnginePower,
                EnginePowerType = vehicle.EnginePowerType,
                Id = vehicle.Id,
                ModelName = vehicle.ModelName,
                Year = vehicle.Year
            });
        }

        return result;
    }
}
