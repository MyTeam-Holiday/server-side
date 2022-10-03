using Microsoft.AspNetCore.Mvc;
using myteam.holiday.EntityFramework.Services;
using myteam.holiday.Domain.Models;
using myteam.holiday.Domain.Services;

namespace myteam.holiday.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IGenericAppDbService<WeatherForecast> _appDbService;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IGenericAppDbService<WeatherForecast> appDbService)
        {
            _logger = logger;
            _appDbService = appDbService;
        }

        [HttpGet("GetWeatherForecast")]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetAsync()
        {
            return Ok(await _appDbService.GetAllValues());
        }

        [HttpPost("AddWeatherForecast")]
        public async Task<ActionResult<int>> PostAsync(WeatherForecast forecast)
        {
            return Ok(await _appDbService.Create(forecast));
        }
    }
}