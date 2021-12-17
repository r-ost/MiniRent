﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniRent.Infrastructure.CarRentalApi.GetVehicles;
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
}