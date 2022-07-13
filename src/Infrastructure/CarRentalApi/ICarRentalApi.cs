using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MiniRent.Application.Common.Interfaces;
using MiniRent.Infrastructure.CarRentalApi.CheckPrice;
using MiniRent.Infrastructure.CarRentalApi.GetVehicles;
using MiniRent.Infrastructure.CarRentalApi.RentVehicle;
using MiniRent.Infrastructure.CarRentalApi.TestIdentity;
using Refit;

namespace MiniRent.Infrastructure.CarRentalApi;

public interface ICarRentalApi
{
    [Get("/vehicles")]
    Task<GetVehiclesResponse> GetVehiclesAsync();

    [Get("/test/identity")]
    [Headers("Authorization: Bearer")]
    Task<IEnumerable<TestIdentityResponseItem>> GetTestIdentity();

    [Post("/vehicle/{brand}/{model}/GetPrice")]
    [Headers("Authorization: Bearer")]
    Task<CheckPriceResponse> GetPriceAsync([AliasAs("brand")] string brand,
        [AliasAs("model")] string model,
        CheckPriceRequest request);

    [Post("/vehicle/{id}/GetPrice")]
    [Headers("Authorization: Bearer")]
    Task<CheckPriceResponse> GetPriceAsync([AliasAs("id")] Guid id,
      CheckPriceRequest request);

    [Post("/vehicles/Rent/{quoteId}")]
    [Headers("Authorization: Bearer")]
    Task<RentVehicleResponse> RentVehicleAsync([AliasAs("quoteId")] Guid quoteId, RentVehicleRequest request);

    [Post("/vehicle/Return/{rentId}")]
    [Headers("Authorization: Bearer")]
    Task ReturnCarAsync([AliasAs("rentId")] Guid rentId);
}
