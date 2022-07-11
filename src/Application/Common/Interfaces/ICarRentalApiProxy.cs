using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniRent.Application.Rentals.Commands.RentCar;
using MiniRent.Application.Rentals.Queries;
using MiniRent.Application.Rentals.Queries.GetPriceWithBrandAndModel;
using MiniRent.Application.Vehicles.Queries.GetVehicles;

namespace MiniRent.Application.Common.Interfaces;

public interface ICarRentalApiProxy
{
    Task<List<VehicleDto>> GetVehiclesAsync();
    Task<PriceDto> GetPriceAsync(string location, int rentDuration, string brand, string model, string company);
    Task<PriceDto> GetPriceAsync(string location, int rentDuration, Guid id, string company);
    Task<RentCarDto> RentCar(string company, Guid carId, DateTime startDate, DateTime endDate, string location);
    Task ReturnCar(Guid rentId, string company);
    Task<VehicleDto> GetVehicleById(Guid vehicleId, string company);
}
