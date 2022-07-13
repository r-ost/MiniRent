using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiniRent.Infrastructure.CarRentalApi.GetVehicles;

public record GetVehiclesResponse
{
    [JsonPropertyName("vehiclesCount")]
    public int VehiclesCount { get; init; }

    [JsonPropertyName("generateDate")]
    public DateTime GenerateDate { get; init; }

    [JsonPropertyName("vehicles")]
    public IReadOnlyList<Vehicle> Vehicles { get; init; } = new List<Vehicle>();
}
