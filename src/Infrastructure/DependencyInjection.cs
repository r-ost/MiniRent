using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace CleanArchitecture.Infrastructure;

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


        services.AddScoped<IDomainEventService, DomainEventService>();
          services.AddTransient<IDateTime, DateTimeService>();


        services.AddScoped<IMiniRentDbContext, MiniRentDbContext>();

        services.AddMicrosoftIdentityWebApiAuthentication(configuration, "AzureAd");

        services.AddAuthorization(options =>
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        return services;
    }
}
