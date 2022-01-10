using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniRent.Application.Rentals.Queries.GetPrice;
using MiniRent.Application.Vehicles.Queries.GetVehicles;

namespace MiniRent.Application.Common.Interfaces;

public interface ICarRentalApiProxy
{
    Task<List<VehicleDto>> GetVehiclesAsync();
    Task<PriceDto> GetPriceAsync(string location, int rentDuration, string brand, string model);
}
