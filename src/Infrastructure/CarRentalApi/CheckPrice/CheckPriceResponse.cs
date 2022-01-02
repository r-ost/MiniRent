using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiniRent.Infrastructure.CarRentalApi.CheckPrice;

public record CheckPriceResponse
{
    [JsonPropertyName("price")]
    public decimal Price { get; init; }

    [JsonPropertyName("currency")]
    public string? Currency { get; init; }

    [JsonPropertyName("generatedAt")]
    public DateTime GeneratedAt { get; init; }

    [JsonPropertyName("expiredAt")]
    public DateTime ExpiredAt { get; init; }

    [JsonPropertyName("quotaId")]
    public Guid QuotaId { get; init; }
}
