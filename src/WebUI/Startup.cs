using CleanArchitecture.Infrastructure;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using MiniRent.Application;
using MiniRent.Application.Common.Interfaces;
using MiniRent.Infrastructure;
using MiniRent.Infrastructure.Persistence;
using MiniRent.WebUI.Filters;
using MiniRent.WebUI.Services;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace MiniRent.WebUI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication();
        services.AddInfrastructure(Configuration);


        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();


        services.AddControllersWithViews(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>())
            .AddFluentValidation(x => x.AutomaticValidationEnabled = false);


        services.AddHealthChecks()
            .AddDbContextCheck<MiniRentDbContext>();


        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        // In production, the Angular files will be served from this directory
        services.AddSpaStaticFiles(configuration =>
            configuration.RootPath = "ClientApp/dist");

        services.AddOpenApiDocument(configure =>
        {
            configure.Title = "MiniRent API";
            configure.AddSecurity("oauth2", new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.OAuth2,
                Name = "Authorization",
                Flow = OpenApiOAuth2Flow.Application,
                Flows = new OpenApiOAuthFlows()
                {
                    AuthorizationCode = new OpenApiOAuthFlow()
                    {
                        Scopes = new Dictionary<string, string>
                        {
                                { Configuration["SwaggerUIDefaultScope"], "Access the api as a signedin user" }
                        },
                        AuthorizationUrl = "https://login.microsoftonline.com/95580983-0258-4189-8b2a-01286b6d5df1/oauth2/v2.0/authorize",
                        TokenUrl = "https://login.microsoftonline.com/95580983-0258-4189-8b2a-01286b6d5df1/oauth2/v2.0/token"
                    },
                },
                Description = "Type into the textbox: Bearer {your JWT token}."
            });

            configure.OperationProcessors.Add(new OperationSecurityScopeProcessor("oauth2"));
        });

        services.AddCors(options =>
        {
            options.AddPolicy(name: "Frontend app",
                              builder =>
                              {
                                  builder.WithOrigins(Configuration["FrontendURL"])
                                    .AllowAnyMethod()
                                    .AllowAnyHeader();
                              });
        });
    }



    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHealthChecks("/health");
        app.UseHttpsRedirection();


        app.UseStaticFiles();
        if (!env.IsDevelopment())
        {
            app.UseSpaStaticFiles();
        }

        app.UseOpenApi();
        app.UseSwaggerUi3(settings =>
        {
            settings.OAuth2Client = new NSwag.AspNetCore.OAuth2ClientSettings()
            {
                ClientId = Configuration["SwaggerUIClientId"],
                UsePkceWithAuthorizationCodeGrant = true,
                ClientSecret = null
            };
            settings.OAuth2Client.UsePkceWithAuthorizationCodeGrant = true;
            settings.Path = "/api";
            settings.DocumentPath = "/api/specification.json";
        });



        app.UseRouting();
        app.UseCors("Frontend app");

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");
        });

        app.UseSpa(spa =>
        {
            // To learn more about options for serving an Angular SPA from ASP.NET Core,
            // see https://go.microsoft.com/fwlink/?linkid=864501

            spa.Options.SourcePath = "ClientApp";

            if (env.IsDevelopment())
            {
                //spa.UseAngularCliServer(npmScript: "start");
                spa.UseProxyToSpaDevelopmentServer(Configuration["SpaBaseUrl"]);
            }
        });
    }
}
