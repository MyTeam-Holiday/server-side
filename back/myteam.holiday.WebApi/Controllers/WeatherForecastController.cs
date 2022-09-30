using Microsoft.AspNetCore.Mvc;
using myteam.holiday.WebServer.Model;
using myteam.holiday.WebServer.Services;

namespace myteam.holiday.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly AppDbWeatherForecastService _forecastService;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            AppDbWeatherForecastService forecastService)
        {
            _logger = logger;
            _forecastService = forecastService;
        }

        [HttpGet("GetWeatherForecast")]
        public async Task<ActionResult<WeatherForecast>> GetAsync()
        {
            return Ok(await _forecastService.GetAllAsync());
        }
    }
}