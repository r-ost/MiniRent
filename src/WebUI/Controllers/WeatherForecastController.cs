using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using MiniRent.Application.WeatherForecasts.Queries.GetWeatherForecasts;

namespace MiniRent.WebUI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{

    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        return await Mediator.Send(new GetWeatherForecastsQuery());
    }


    [HttpGet("credentials")]
    public IEnumerable<string> GetMyCredentials()
    {
        var principal = HttpContext.User;
        var result = new List<string>();
        if (principal?.Claims != null)
        {
            foreach (var claim in principal.Claims)
            {
                result.Add(claim.Value);
            }
        }
        return result;
    }


    [HttpGet("{id}")]
    public async Task<IEnumerable<WeatherForecast>> Get(int id)
    {
        return await Mediator.Send(new GetWeatherForecastsQuery());
    }


    [HttpPost]
    public async Task Post()
    {
        await Mediator.Send(new GetWeatherForecastsQuery());
    }
}
