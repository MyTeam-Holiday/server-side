using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using myteam.holiday.EntityFramework.Data;

namespace myteam.holiday.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContextController : Controller
    {
        readonly AppDbContextFactory _contextFactory;
        public ContextController(AppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        [HttpGet,Route("CreateSqlServerdbContext")]
        public async Task<IActionResult> CreateSqlServerdbContext(string connectionString)
        {
            AppDbContext db = _contextFactory.CreateSqlServerdbContext(connectionString);
            await db.Database.EnsureCreatedAsync();

            if (await db.Database.CanConnectAsync())
                return Ok("");
            else
                return BadRequest("");
        }

        [HttpGet, Route("CreateMariadbContext")]
        public async Task<IActionResult> CreateMariadbContext(string connectionString, string version)
        {
            AppDbContext db = _contextFactory.CreateMariadbContext(connectionString,new Version(version));
            await db.Database.EnsureCreatedAsync();

            if (await db.Database.CanConnectAsync())
                return Ok("");
            else
                return BadRequest("");
        }
    }
}
