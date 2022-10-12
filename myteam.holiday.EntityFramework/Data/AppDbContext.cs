using Microsoft.EntityFrameworkCore;
using myteam.holiday.Domain.Models;

namespace myteam.holiday.EntityFramework.Data
{
    public class AppDbContext : DbContext
    {
        internal DbSet<Base> Base => Set<Base>();
        internal DbSet<Holiday> Holiday => Set<Holiday>();
        internal DbSet<Team> Team => Set<Team>();
        internal DbSet<TeamHoliday> TeamHoliday => Set<TeamHoliday>();
        internal DbSet<TeamUser> TeamUser => Set<TeamUser>();
        internal DbSet<User> User => Set<User>();
        internal DbSet<UserRole> UserRole => Set<UserRole>();
        internal DbSet<UserGuId> UserGuId => Set<UserGuId>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    }
}
