using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MiniRent.Application.Common.Interfaces;

namespace MiniRent.Application.Rentals.Commands.ReturnCar;

public class ReturnCarCommand : IRequest<int>
{
    public Guid RentId { get; set; }
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
        await _miniRentDbContext.SaveChangesAsync(cancellationToken);

        return 1;
    }
}
