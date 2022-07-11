﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MiniRent.Application.Common.Interfaces;
using MiniRent.Application.Rentals.Queries.GetCurrentRentals;

namespace MiniRent.Application.Rentals.Queries.GetHistoricRentals;

public class GetHistoricRentalsQuery : IRequest<IEnumerable<HistoricRentalDto>>
{
}


public class GetHistoricRentalsQueryHandler :IRequestHandler<GetHistoricRentalsQuery, IEnumerable<HistoricRentalDto>>
{
    private readonly IMiniRentDbContext _miniRentDbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly ICarRentalApiProxy _carRentalApiProxy;

    public GetHistoricRentalsQueryHandler(IMiniRentDbContext miniRentDbContext,
        ICurrentUserService currentUserService, ICarRentalApiProxy carRentalApiProxy)
    {
        _miniRentDbContext = miniRentDbContext;
        _currentUserService = currentUserService;
        _carRentalApiProxy = carRentalApiProxy;
    }


    public async Task<IEnumerable<HistoricRentalDto>> Handle(GetHistoricRentalsQuery request, CancellationToken cancellationToken)
    {
        var userLogin = _currentUserService.Login;

        var rents = _miniRentDbContext.Rents
            .Where(x => x.Renter.Login.Email == userLogin && x.RentStatus == Domain.Enums.RentStatus.Completed)
            .ToList();

        var vehicles = await _carRentalApiProxy.GetVehiclesAsync();

        return rents.Select(x =>
        {
            var car = vehicles.FirstOrDefault(v => v.Id.ToString() == x.CarId);
            return (rent: x, car);

        })
        .Where(x => x.car is not null)
        .Select(x =>
        {
            return new HistoricRentalDto()
            {
                CarDetailsDto = new CarDetailsDto()
                {
                    Brand = x.car!.BrandName ?? "",
                    Model = x.car!.ModelName ?? "",
                    Capacity = x.car!.Capacity,
                    Description = x.car!.Description ?? "",
                    EnginePower = x.car!.EnginePower,
                    EnginePowerType = x.car!.EnginePowerType ?? "",
                    YearOfProduction = x.car!.Year,
                },
                DateFrom = x.rent.StartDate ?? new DateTime(),
                DateTo = x.rent.EndDate ?? new DateTime(),
                RentId = new Guid(x.rent.RentId),
                RentCompanyName = x.rent.Company.Name,
                TotalPrice = x.rent.TotalPrice,
                Currency = x.rent.Currency
            };
        });
    }
}
