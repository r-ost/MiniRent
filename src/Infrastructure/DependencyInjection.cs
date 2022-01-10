using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using MiniRent.Application.Common.Interfaces;
using MiniRent.Infrastructure.CarRentalApi;
using MiniRent.Infrastructure.Persistence;
using MiniRent.Infrastructure.Services;
using Refit;

namespace MiniRent.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<MiniRentDbContext>(options =>
                options.UseInMemoryDatabase("MiniRentDb"));
        }
        else
        {
            services.AddDbContext<MiniRentDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(MiniRentDbContext).Assembly.FullName)));
        }

        // https://mindbyte.nl/2021/06/02/simple-oauth2-api-authentication-with-token-caching-and-refetching-in-an-azure-function-using-identitymodel-and-refit.html
        services.AddAccessTokenManagement(options =>
        {
            options.Client.Clients.Add("lecturer-api", new ClientCredentialsTokenRequest
            {
                RequestUri = new Uri(new Uri(configuration["CarRentalApi:IdentityProvider:BaseAddress"]),
                    new Uri(configuration["CarRentalApi:IdentityProvider:TokenEndpoint"], UriKind.Relative)),
                ClientId = configuration["CarRentalApi:client_id"],
                ClientSecret = configuration["CarRentalApi:client_secret"]
            });
        });


        services.AddRefitClient<ILecturerCarRentalApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(configuration["CarRentalApi:BaseAddress"]);
            })
            .AddClientAccessTokenHandler("lecturer-api");


        services.AddScoped<ICarRentalApiProxy>(provider
            => new CarRentalApiProxy(provider.GetRequiredService<ILecturerCarRentalApi>(),
            provider.GetRequiredService<IMiniRentDbContext>(),
            provider.GetRequiredService<ICurrentUserService>(),
            provider.GetRequiredService<IDateTime>()));

        services.AddScoped<IDomainEventService, DomainEventService>();
        services.AddTransient<IDateTime, DateTimeService>();

        services.AddScoped<IMiniRentDbContext, MiniRentDbContext>();

        services.AddMicrosoftIdentityWebApiAuthentication(configuration, "AzureAd");

        services.AddAuthorization(options =>
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        return services;
    }
}
