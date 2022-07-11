using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniRent.Application.Common.Interfaces;
using MiniRent.Application.Rentals.Commands.RentCar;
using MiniRent.Application.Rentals.Queries;
using MiniRent.Application.Vehicles.Queries.GetVehicles;
using MiniRent.Domain.Entities;
using MiniRent.Infrastructure.CarRentalApi.CheckPrice;
using MiniRent.Infrastructure.CarRentalApi.GetVehicles;
using MiniRent.Infrastructure.CarRentalApi.RentVehicle;

namespace MiniRent.Infrastructure.CarRentalApi;

public class CarRentalApiProxy : ICarRentalApiProxy
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMiniRentDbContext _miniRentDbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTime _dateTime;
    private readonly IDictionary<string, ICarRentalApi> _carRentalApis = new Dictionary<string, ICarRentalApi>();


    public CarRentalApiProxy(IHttpClientFactory httpClientFactory,
        IEnumerable<string> carRentalApisNames,
        IMiniRentDbContext miniRentDbContext,
        ICurrentUserService currentUserService,
        IDateTime dateTime)
    {
        _httpClientFactory = httpClientFactory;

        foreach (var apiName in carRentalApisNames)
        {
            _carRentalApis.Add(apiName, Refit.RestService.For<ICarRentalApi>(httpClientFactory.CreateClient(apiName)));
        }
        _httpClientFactory = httpClientFactory;
        _miniRentDbContext = miniRentDbContext;
        _currentUserService = currentUserService;
        _dateTime = dateTime;
    }

    public async Task<PriceDto> GetPriceAsync(string location, int rentDuration, string brand, string model, string company)
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

        var price = await _carRentalApis[company].GetPriceAsync(brand, model,request);

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

    public async Task<PriceDto> GetPriceAsync(string location, int rentDuration, Guid id, string company)
    {
        var user = _miniRentDbContext.Renters
           .Where(x => x.Login.Email == _currentUserService.Login)
           .First();

        // TODO - calculate currently and overall rented counts
        var request = GetCheckPriceRequest(user, location, rentDuration);

        var price = await _carRentalApis[company].GetPriceAsync(id, request);

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

    private CheckPriceRequest GetCheckPriceRequest(Renter user, string location, int rentDuration)
    {
        return new CheckPriceRequest()
        {
            Age = (_dateTime.Now - (user.DateOfBirth ?? new DateTime())).Days / 365,
            CurrentlyRentedCount = 0,
            OverallRentedCount = 0,
            Location = location,
            RentDuration = rentDuration,
            YearsOfHavingDriverLicense = _dateTime.Now.Year - user.LicenceObtainmentYear
        };
    }

    public async Task<List<VehicleDto>> GetVehiclesAsync()
    {
        // iterate through all apis and return results
        var vehiclesResponses = new List<(string companyName, GetVehiclesResponse response)>();
        foreach (var api in _carRentalApis)
        {
            var vehiclesResponse = await api.Value.GetVehiclesAsync();
            vehiclesResponses.Add((api.Key, vehiclesResponse));
        }

        List<VehicleDto> result = new(vehiclesResponses.Sum(x => x.response.VehiclesCount));

        foreach (var vehicleResponse in vehiclesResponses)
        {
            var company = _miniRentDbContext.Companies
                .Where(x => x.Name == vehicleResponse.companyName)
                .FirstOrDefault();

            foreach (var vehicle in vehicleResponse.response.Vehicles)
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
                        RentCompany = company?.Name ?? "",
                        RentCompanyId = company?.Id ?? -1
                    });
            }
        }

        return result;
    }

    public async Task<VehicleDto> GetVehicleById(Guid vehicleId, string company)
    {
        var vehicles = await GetVehiclesAsync();

        return vehicles.First(x => x.Id == vehicleId);
    }

    public async Task<RentCarDto> RentCar(string company, Guid carId, DateTime startDate, DateTime endDate, string location)
    {   
        var rentDuration = (int)(endDate - startDate).TotalDays;
        var priceDto = await GetPriceAsync(location, rentDuration, carId, company);

        var response = await _carRentalApis[company].RentVehicleAsync(priceDto.QuotaId,
            new RentVehicleRequest() { StartDate = startDate});

        var companyEntity = _miniRentDbContext.Companies
            .Where(x => x.Name == company)
            .FirstOrDefault();

        return new RentCarDto()
        {
            RentId = response.RentId,
            QuoteId = response.QuoteId,
            StartDate = response.StartDate,
            EndDate = response.EndDate,
            RentAt = response.RentAt,
            RentCompanyId = companyEntity?.Id ?? -1,
            TotalPrice = priceDto.Price,
            Currency = priceDto.Currency ?? ""
        };
    }

    public async Task ReturnCar(Guid rentId, string company)
    {
        await _carRentalApis[company].ReturnCarAsync(rentId);
    }
}
