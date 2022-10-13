using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myteam.holiday.EntityFramework.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        private IConfiguration _configuration;

        public AppDbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public AppDbContext CreateDbContext(string[] args = null!)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>();
            options.UseMySql(_configuration.GetConnectionString("DefaultConnection")
<<<<<<< HEAD
                ?? "server=localhost;user id=admin;password=12345;database=TeamHoliday",
=======
                ?? "server=localhost;user id=root;password=12345;database=dev.myteam.holiday",
>>>>>>> 43c2624
                new MySqlServerVersion(new Version(10, 10, 1)));
                
            return new AppDbContext(options.Options);
        }
    }
}
