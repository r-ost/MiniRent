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
    Task<PriceDto> GetPriceAsync(string location, int rentDuration, string brand, string model);
    Task<PriceDto> GetPriceAsync(string location, int rentDuration, Guid id);
    Task<RentCarDto> RentCar(Guid quoteId, DateTime startDate);
    Task ReturnCar(Guid rentId);
    Task<VehicleDto> GetVehicleById(Guid vehicleId);
}
