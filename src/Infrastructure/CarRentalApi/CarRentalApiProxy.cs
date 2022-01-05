using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniRent.Application.Common.Interfaces;
using MiniRent.Application.Vehicles.Queries.GetVehicles;
using MiniRent.Infrastructure.CarRentalApi.GetVehicles;

namespace MiniRent.Infrastructure.CarRentalApi;

public class CarRentalApiProxy : ICarRentalApiProxy
{
    private readonly ILecturerCarRentalApi _lecturerCarRentalApi;


    public CarRentalApiProxy(ILecturerCarRentalApi lecturerCarRentalApi)
    {
        _lecturerCarRentalApi = lecturerCarRentalApi;
    }

    public async Task<List<VehicleDto>> GetVehiclesAsync()
    {
        // iterate through all apis and return results
        var lecturerApiVehicles = await _lecturerCarRentalApi.GetVehiclesAsync();


        List<VehicleDto> result = new(lecturerApiVehicles.VehiclesCount);
        foreach (var vehicle in lecturerApiVehicles.Vehicles)
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
                Year = vehicle.Year,
                RentCompany = "Lecturer car rental company"
            });
        }
        
        return result;
    }
}
