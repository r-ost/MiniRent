using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MiniRent.Application.Vehicles.Queries.CheckPriceWithBrandAndModel;

public class CheckPriceWithBrandAndModelQuery : IRequest<CheckPriceWithBrandAndModelDto>
{
    public string Brand { get; set; } = "";
    public string Model { get; set; } = "";
}

public class CheckPriceQueryHandler : IRequestHandler<CheckPriceWithBrandAndModelQuery, CheckPriceWithBrandAndModelDto>
{
    public Task<CheckPriceWithBrandAndModelDto> Handle(CheckPriceWithBrandAndModelQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
