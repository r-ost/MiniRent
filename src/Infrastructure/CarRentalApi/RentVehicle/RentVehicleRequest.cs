using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiniRent.Infrastructure.CarRentalApi.RentVehicle;

public record RentVehicleRequest
{
    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; init; }
}
