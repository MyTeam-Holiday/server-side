using Microsoft.EntityFrameworkCore;
using myteam.holiday.WebServer.Model;

namespace myteam.holiday.WebServer.Data
{
    public class AppDbContext : DbContext
    {
        internal DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();
        internal DbSet<Group> Group => Set<Group>();
        internal DbSet<User> User => Set<User>();
        internal DbSet<GroupRole> GroupRole => Set<GroupRole>();
        internal DbSet<UserRole> UserRole => Set<UserRole>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    }
}
