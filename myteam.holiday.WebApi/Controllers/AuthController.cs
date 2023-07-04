using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using myteam.holiday.Domain.Services;
using System.Security.Claims;

namespace myteam.holiday.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AuthController : ControllerBase
    {       
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet, Route("GoogleOAuth")]
        public IActionResult GoogleOAuth()
        {
            return Challenge(new AuthenticationProperties
            { RedirectUri = $"https://{HttpContext.Request.Host}/Auth/GoogleLoginCallBack" },
                authenticationSchemes: new string[] { GoogleDefaults.AuthenticationScheme });
        }

        [HttpGet, Route("GoogleLoginCallBack")]
        public async Task<IActionResult> GoogleLoginCallBackAsync()
        {
            var response = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (response == null || !response.Succeeded) Redirect($"https://{HttpContext.Request.Host}/Auth/GoogleOAuth");

            var cp = response!.Principal!.Clone();
            var externalClaims = cp.Claims.ToList();
            var email = externalClaims.First(c => c.Type == ClaimTypes.Email);
            var userName = externalClaims.First(c => c.Type == ClaimTypes.Name);

            var user = await _userRepository.GetUserByEmail(email.Value);
            var claims = new List<Claim> 
            {
                email,
                userName
            };            

            if (user == null) 
            {
                await _userRepository.PreCreateUser(userName.Value, email.Value);
                claims.Add(new Claim(ClaimTypes.Role, "User"));                
            }
            else
            {
                //добавить юзеру свойство роли
                claims.Add(new Claim(ClaimTypes.Role, "Moderator"));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return LocalRedirect("/swagger/index.html");
        }
    }
}
