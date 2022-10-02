using Microsoft.EntityFrameworkCore;
using myteam.holiday.Domain.Models;
using myteam.holiday.WebServer.Model;

namespace myteam.holiday.WebServer.Data
{
    public class AppDbContext : DbContext
    {
        internal DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();
        internal DbSet<Celebration> Group => Set<Celebration>();
        internal DbSet<Medical> Medical => Set<Medical>();
        internal DbSet<Team> Team => Set<Team>();
        internal DbSet<User> User => Set<User>();
        internal DbSet<UserRole> UserRole => Set<UserRole>();
        internal DbSet<Vacation> Vacation => Set<Vacation>();
        internal DbSet<Weekend> Weekend => Set<Weekend>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    }
}
