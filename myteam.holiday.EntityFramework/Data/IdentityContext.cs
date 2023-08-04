using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using myteam.holiday.Domain.Models;

namespace myteam.holiday.EntityFramework.Data
{
    public class IdentityContext : IdentityDbContext<AppUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) 
        {
        }

        //сделать пример переименованной таблицы
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<AppUser>().ToTable("Users");
            //builder.Entity<IdentityRole>().ToTable("Roles");
            //builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        }
    }
}
