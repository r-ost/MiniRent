using CleanArchitecture.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;

public class WeatherForecastController : ApiControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        return await Mediator.Send(new GetWeatherForecastsQuery());
    }


    [HttpGet("{id}")]
    public async Task<IEnumerable<WeatherForecast>> Get(int id)
    {
        return await Mediator.Send(new GetWeatherForecastsQuery());
    }
}
