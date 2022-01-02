using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiniRent.Infrastructure.CarRentalApi.GetVehicles;

public record Vehicle
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("brandName")]
    public string? BrandName { get; init; }

    [JsonPropertyName("modelName")]
    public string? ModelName { get; init; }

    [JsonPropertyName("year")]
    public int Year { get; init; }

    [JsonPropertyName("enginePower")]
    public int EnginePower { get; init; }

    [JsonPropertyName("enginePowerType")]
    public string? EnginePowerType { get; init; }

    [JsonPropertyName("capacity")]
    public int Capacity { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }
}
