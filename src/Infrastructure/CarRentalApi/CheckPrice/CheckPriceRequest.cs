using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiniRent.Infrastructure.CarRentalApi.CheckPrice;

public record CheckPriceRequest
{
    [JsonPropertyName("age")]
    public int Age { get; init; }

    [JsonPropertyName("yearsOfHavingDriverLicense")]
    public int YearsOfHavingDriverLicense { get; init; }

    [JsonPropertyName("rentDuration")]
    public int RentDuration { get; init; }

    [JsonPropertyName("location")]
    public string? Location { get; init; }

    [JsonPropertyName("currentlyRentedCount")]
    public int? CurrentlyRentedCount { get; init; }

    [JsonPropertyName("overallRentedCount")]
    public int? OverallRentedCount { get; init; }
}
