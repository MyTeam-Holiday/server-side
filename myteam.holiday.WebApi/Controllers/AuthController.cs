using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace myteam.holiday.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet, Route("GoogleOAuth")]
        public IActionResult GoogleOAuth()
        {
            return Challenge(new AuthenticationProperties
            { RedirectUri = "https://localhost:5001/swagger/index.html" },
                authenticationSchemes: new string[] { "google" });
        }
    }
}
