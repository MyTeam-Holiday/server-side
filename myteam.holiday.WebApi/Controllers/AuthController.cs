using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using myteam.holiday.Domain.Models;
using myteam.holiday.Domain.Services;
using myteam.holiday.WebApi.EmailService;
using myteam.holiday.WebApi.Models;
using System.Security.Claims;
using System.Security.Cryptography;

namespace myteam.holiday.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]    
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;        

        public AuthController(IUserRepository userRepository, IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _emailSender = emailSender;
        }

        [HttpPost, Route("Registration")]
        public async Task<IActionResult> Registration([FromBody] UserRegDto regModel)
        {

            var user = await _userRepository.GetUserByEmail(regModel.UserEmail!);
            if (user != null) return BadRequest("User already exists");

            var hashPassword = CreateHash(regModel.Password);
            var token = CreateRandomToken();

            var userGuId = await _userRepository.CreateUser(new User
            {
                UserName = regModel.UserName,
                UserEmail = regModel.UserEmail,
                PasswordHash = hashPassword,
                //VerifyToken = CreateRandomToken().Split('=')[1],
                //HasVerified = false
            });

            await _emailSender.SendVerifyTokenAsync(token, regModel.UserEmail!);
            return Ok();
        }

        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginModel)
        {
            var user = await _userRepository.GetUserByEmail(loginModel.UserEmail!);
            if (user == null) return BadRequest("Email or password is invalid");

            if (!IsPasswordVerified(loginModel.Password, user.PasswordHash)) BadRequest("Email or password is invalid"); 
            //if (!user.HasVerified ) return BadRequest("You should confirm your email"); 

            return Ok();
        }

        [HttpPost, Route("Verify")]
        public IActionResult Verify([FromQuery]string token)
        {
            //var user = await _userRepository.FindByToken(token);
            //if (user == null || user.HasVerified) return BadRequest();
            //if (user.VerifyToken == token)
            //{user.HasVerified = true; return Ok();}
            return Ok();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("GoogleOAuth")]
        public IActionResult GoogleOAuth()
        {
            return Challenge(new AuthenticationProperties
            { RedirectUri = $"https://{HttpContext.Request.Host}/Auth/GoogleLoginCallBack" },
                authenticationSchemes: new string[] { GoogleDefaults.AuthenticationScheme });
        }

        [ApiExplorerSettings(IgnoreApi = true)]
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
                //await _userRepository.PreCreateUser(userName.Value, email.Value);
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

        private string CreateHash(string? password) =>
            BCrypt.Net.BCrypt.EnhancedHashPassword(password, workFactor: 12);

        private bool IsPasswordVerified(string? password, string? passwordHash) =>
            BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);

        private string CreateRandomToken() =>            
            $"https://{HttpContext.Request.Host}/Auth/Verify?token={Convert.ToHexString(RandomNumberGenerator.GetBytes(32))}";        
    }
}
