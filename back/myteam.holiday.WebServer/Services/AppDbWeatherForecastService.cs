
using Microsoft.EntityFrameworkCore;
using myteam.holiday.WebServer.Data;
using myteam.holiday.WebServer.Model;

namespace myteam.holiday.WebServer.Services
{
    public sealed class AppDbWeatherForecastService
    {
        private AppDbContext _context;

        public AppDbWeatherForecastService(IDbContextFactory<AppDbContext> contextFactory)
        {
            _context = contextFactory.CreateDbContext();
        }

        public async Task<int> AddAsync(WeatherForecast forecast)
        {
            await _context.AddAsync(forecast);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WeatherForecast>> GetAllAsync()
        {
            List<WeatherForecast> gg = await _context.WeatherForecasts.ToListAsync();
            return await _context.WeatherForecasts.ToListAsync();
        }

        public async Task<WeatherForecast> GetByIdAsync(int id)
        {
            return await _context.WeatherForecasts.FirstOrDefaultAsync(forecast => forecast.Id == id) ?? new();
        }
    }
}
