using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRent.Application.Vehicles.Queries.CheckPriceWithBrandAndModel;

public class CheckPriceWithBrandAndModelDto
{
    public decimal Price { get; set; }

    public string? Currency { get; set; }

    public DateTime GeneratedAt { get; set; }

    public DateTime ExpiredAt { get; set; }

    public Guid QuotaId { get; set; }
}
