using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MiniRent.Application.Common.Interfaces;
using MiniRent.Domain.Entities;
using MiniRent.Domain.Enums;
using MiniRent.Domain.ValueObjects;

namespace MiniRent.Application.Rentals.Commands.RentCar;

public class RentCarCommand : IRequest<RentCarDto>
{
    public Guid QuoteId { get; set; }
    public Guid CarId { get; set; }
    public DateTime StartDate { get; set; }
}

public class RentCarCommandHandler : IRequestHandler<RentCarCommand, RentCarDto>
{
    private readonly ICarRentalApiProxy _carRentalApiProxy;
    private readonly IMiniRentDbContext _miniRentDbContext;
    private readonly ICurrentUserService _currentUserService;

    public RentCarCommandHandler(ICarRentalApiProxy carRentalApiProxy,
        IMiniRentDbContext miniRentDbContext,
        ICurrentUserService currentUserService)
    {
        _carRentalApiProxy = carRentalApiProxy;
        _miniRentDbContext = miniRentDbContext;
        _currentUserService = currentUserService;
    }

    public async Task<RentCarDto> Handle(RentCarCommand request, CancellationToken cancellationToken)
    {
        var result = await _carRentalApiProxy.RentCar(request.QuoteId, request.StartDate);
        var renter = _miniRentDbContext.Renters.First(x => x.Login.Email == _currentUserService.Login);
        var cars = await _carRentalApiProxy.GetVehiclesAsync();
        var car = cars.First(x => x.Id == request.CarId);

        _miniRentDbContext.Rents
            .Add(new Rent()
            {
                RenterId = renter.Id,
                RentId = result.RentId.ToString(),
                StartDate = result.StartDate,
                EndDate = result.EndDate,
                CarId = car.Id.ToString(),
                CompanyId = result.RentCompanyId,
                RentAt = result.RentAt,
                RentStatus = RentStatus.Pending
            });
        await _miniRentDbContext.SaveChangesAsync(cancellationToken);
        return result;
    }
}