using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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
        public AppDbContext CreateDbContext(string[] args = null!)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>();
            options.UseMySql("server=localhost;user id=root;password=12345;database=dev.myteam.holiday",
                new MySqlServerVersion(new Version(10, 10, 1)));
                
            return new AppDbContext(options.Options);
        }
    }
}
