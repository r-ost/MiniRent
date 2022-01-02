using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiniRent.Infrastructure.CarRentalApi.RentVehicle;

public record RentVehicleResponse
{
    [JsonPropertyName("quoteId")]
    public Guid QuoteId { get; init; }

    [JsonPropertyName("rentId")]
    public Guid RentId { get; init; }

    [JsonPropertyName("rentAt")]
    public DateTime RentAt { get; init; }

    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; init; }

    [JsonPropertyName("endDate")]
    public DateTime EndDate { get; init; }
}
