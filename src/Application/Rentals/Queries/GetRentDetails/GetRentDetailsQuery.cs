using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MiniRent.Application.Common.Interfaces;
using MiniRent.Application.Rentals.Queries.GetCurrentRentals;
using MiniRent.Domain.Enums;

namespace MiniRent.Application.Rentals.Queries.GetRentDetails;

public class GetRentDetailsQuery : IRequest<RentDetailsDto>
{
    public Guid RentId { get; set; }
}


public class GetRentDetailsQueryHandler : IRequestHandler<GetRentDetailsQuery, RentDetailsDto>
{
    private readonly IMiniRentDbContext _miniRentDbContext;
    private readonly ICarRentalApiProxy _carRentalApiProxy;

    public GetRentDetailsQueryHandler(IMiniRentDbContext miniRentDbContext, ICarRentalApiProxy carRentalApiProxy)
    {
        _miniRentDbContext = miniRentDbContext;
        _carRentalApiProxy = carRentalApiProxy;
    }

    public async Task<RentDetailsDto> Handle(GetRentDetailsQuery request, CancellationToken cancellationToken)
    {
        var rent = _miniRentDbContext.Rents
            .First(r => r.RentId == request.RentId.ToString() && r.RentStatus == RentStatus.Completed);
        
        var rentDetails = _miniRentDbContext.FinalizedRentDetails
            .First(d => d.Rent.RentId == rent.RentId);
        var vehicles = await _carRentalApiProxy.GetVehiclesAsync();
        var car = vehicles.First(v => v.Id.ToString() == rent.CarId);


        return new RentDetailsDto
        {
            CarDetailsDto = new CarDetailsDto()
            {
                Brand = car.BrandName ?? "",
                Model = car.ModelName ?? "",
                Capacity = car.Capacity,
                Description = car.Description ?? "",
                EnginePower = car.EnginePower,
                EnginePowerType = car.EnginePowerType ?? "",
                YearOfProduction = car.Year,
            },
            DateFrom = rent.StartDate ?? new DateTime(),
            DateTo = rent.EndDate ?? new DateTime(),
            RentId = new Guid(rent.RentId),
            RentCompanyName = rent.Company.Name,
            TotalPrice = rent.TotalPrice,
            Currency = rent.Currency,
            UserName = rent.Renter.Login.Email,
            Description = rentDetails.Description,
            Location = rent.Location,
            OdometerValueInKm = rentDetails.OdometerValueInKm,
            OverallState = rentDetails.OverallState,
            PhotoPath = rentDetails.PhotoPath,
            ProtocolPath = rentDetails.ProtocolPath
        };
    }
}
