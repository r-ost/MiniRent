using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniRent.Application.Common.Interfaces;
using MiniRent.Application.Rentals.Queries.GetPrice;
using MiniRent.Application.Vehicles.Queries.GetVehicles;
using MiniRent.Infrastructure.CarRentalApi.CheckPrice;
using MiniRent.Infrastructure.CarRentalApi.GetVehicles;

namespace MiniRent.Infrastructure.CarRentalApi;

public class CarRentalApiProxy : ICarRentalApiProxy
{
    private readonly ILecturerCarRentalApi _lecturerCarRentalApi;
    private readonly IMiniRentDbContext _miniRentDbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTime _dateTime;

    public CarRentalApiProxy(ILecturerCarRentalApi lecturerCarRentalApi,
        IMiniRentDbContext miniRentDbContext,
        ICurrentUserService currentUserService,
        IDateTime dateTime)
    {
        _lecturerCarRentalApi = lecturerCarRentalApi;
        _miniRentDbContext = miniRentDbContext;
        _currentUserService = currentUserService;
        _dateTime = dateTime;
    }

    public async Task<PriceDto> GetPriceAsync(string location, int rentDuration, string brand, string model)
    {
        var user = _miniRentDbContext.Renters
            .Where(x => x.Login.Email == _currentUserService.Login)
            .First();

        // TODO - calculate currently and overall rented counts
        var request = new CheckPriceRequest()
        {
            Age = (_dateTime.Now - (user.DateOfBirth ?? new DateTime())).Days / 365,
            CurrentlyRentedCount = 0,
            OverallRentedCount = 0,
            Location = location,
            RentDuration = rentDuration,
            YearsOfHavingDriverLicense = _dateTime.Now.Year - user.LicenceObtainmentYear
        };

        var price = await _lecturerCarRentalApi.GetPriceAsync(brand, model,request);

        var priceDto = new PriceDto()
        {
            Currency = price.Currency,
            ExpiredAt = price.ExpiredAt,
            GeneratedAt = price.GeneratedAt,
            Price = price.Price,
            QuotaId = price.QuotaId
        };
        return priceDto;
    }

    public async Task<List<VehicleDto>> GetVehiclesAsync()
    {
        // iterate through all apis and return results
        var lecturerApiVehicles = await _lecturerCarRentalApi.GetVehiclesAsync();


        List<VehicleDto> result = new(lecturerApiVehicles.VehiclesCount);
        var companyName = "Lecturer car rental company";
        var company = _miniRentDbContext.Companys
            .Where(x => x.Name == companyName)
            .FirstOrDefault();

        if (company is null)
        {
            return new List<VehicleDto>();
        }

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
                RentCompany = company.Name,
                RentCompanyId = company.Id
            });
        }

        return result;
    }
}
