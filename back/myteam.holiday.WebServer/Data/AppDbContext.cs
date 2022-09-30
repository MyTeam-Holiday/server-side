using Microsoft.EntityFrameworkCore;
using myteam.holiday.WebServer.Model;

namespace myteam.holiday.WebServer.Data
{
    public class AppDbContext : DbContext
    {
        internal DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    }
}
