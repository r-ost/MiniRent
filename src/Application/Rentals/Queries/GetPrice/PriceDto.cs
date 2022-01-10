using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRent.Application.Rentals.Queries.GetPrice;

public class PriceDto
{
    public decimal Price { get; init; }

    public string? Currency { get; init; }
    public DateTime GeneratedAt { get; init; }

    public DateTime ExpiredAt { get; init; }

    public Guid QuotaId { get; init; }
}
