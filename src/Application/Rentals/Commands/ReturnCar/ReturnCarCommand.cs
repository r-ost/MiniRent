using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MiniRent.Application.Common.Interfaces;
using MiniRent.Domain.Entities;

namespace MiniRent.Application.Rentals.Commands.ReturnCar;

public class ReturnCarCommand : IRequest<int>
{
    public Guid RentId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string OverallState { get; set; } = string.Empty;
    public int OdometerValueInKm { get; set; } 
    // TODO: Documents (photo - .jpg and protocol - .pdf)
}

public class ReturnCarCommandHandler : IRequestHandler<ReturnCarCommand, int>
{
    private readonly IMiniRentDbContext _miniRentDbContext;
    private readonly ICarRentalApiProxy _carRentalApiProxy;

    public ReturnCarCommandHandler(IMiniRentDbContext miniRentDbContext,
        ICarRentalApiProxy carRentalApiProxy)
    {
        _miniRentDbContext = miniRentDbContext;
        _carRentalApiProxy = carRentalApiProxy;
    }

    public async Task<int> Handle(ReturnCarCommand request, CancellationToken cancellationToken)
    {
        var rent = _miniRentDbContext.Rents
            .FirstOrDefault(r => r.RentId == request.RentId.ToString());

        if (rent is null)
        {
            return 0;
        }

        await _carRentalApiProxy.ReturnCar(new Guid(rent.RentId));

        // set rent as completed
        rent.RentStatus = Domain.Enums.RentStatus.Completed;


        _miniRentDbContext.FinalizedRentDetails
            .Add(new FinalizedRentDetails
            {
                Description = request.Description,
                OdometerValueInKm = request.OdometerValueInKm,
                OverallState = request.OverallState,
                RentId = rent.Id,
                PhotoPath = "photo.jpg",
                ProtocolPath = "protocol.pdf",
            });

        await _miniRentDbContext.SaveChangesAsync(cancellationToken);

        return 1;
    }
}
