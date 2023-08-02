using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using myteam.holiday.Domain.Models;
using myteam.holiday.WebApi.EmailService;
using myteam.holiday.WebApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace myteam.holiday.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpPost, Route("Registration")]
        public async Task<IActionResult> Registration([FromBody] UserRegDto regModel)
        {
            var user = new AppUser
            {
                UserName = regModel.UserName,
                Email = regModel.UserEmail
            };

            var result = await _userManager.CreateAsync(user, regModel.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var role = await _userManager.AddToRoleAsync(user, "Visitor");
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = CreateCallBackUrl(user.Email!, token, nameof(ConfirmEmail));
            await _emailSender.SendVerifyTokenAsync(callbackUrl!, user.Email!);
            return Ok("Confirm your email");
        }

        [HttpGet, Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, string token)
        {
            if (email == null || token == null) return BadRequest("Invalid email confirmation url");

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound();

            var result = await _userManager.ConfirmEmailAsync(user, token);

            var status = result.Succeeded ? "Confirmed" : "Not confirmed, try again";
            return Ok(status);
        }

        [HttpPost, Route("RequeueConfirmToken")]
        public async Task<IActionResult> RequeueConfirmToken([Required] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return NotFound("Invalid email");
            if (user.EmailConfirmed) return Ok("Your email is already confirmed");

            if (user.EmailConfirmationAttempts >= 3)
            {
                await _userManager.DeleteAsync(user);
                return BadRequest("Exceeded the number of attempts");
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = CreateCallBackUrl($"{user.Email}", token, nameof(ConfirmEmail));
            await _emailSender.SendVerifyTokenAsync(callbackUrl!, email);
            user.EmailConfirmationAttempts++;
            await _userManager.UpdateAsync(user);
            return Ok("Confirmation token has been sent to your email");
        }

        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.UserEmail);

            if (user == null) return NotFound();

            var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, loginModel.RememberMe, false);
            if (!result.Succeeded) return BadRequest("Email or password are wrong");

            return Ok();
        }

        [HttpPost, Route("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            //должен быть редирект к странице логина
            return Ok();
        }

        [HttpPost, Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return BadRequest("Invalid Email");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = CreateCallBackUrl(user.Email, token, nameof(ResetPass));
            await _emailSender.SendVerifyTokenAsync(callbackUrl!, model.Email!);
            return Ok("Password recovery link has been sent to your email");
        }

        [HttpGet, Route("ResetPassword")]
        public Task<IActionResult> ResetPass([FromQuery] string email, string token)
        {
            var model = new ResetPasswordDto { Email = email, Token = token };
            return Task.FromResult<IActionResult>(Ok(model));
        }

        [HttpPost, Route("UpdatePassword")]
        public async Task<IActionResult> UpdatePass([FromForm] ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return BadRequest("Invalid Email");
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            var status = result.Succeeded ? "Password has been changed" : "Password has not been changed, try again";

            return Ok(status);
        }

        [HttpGet, Route("GoogleLogin")]
        public IActionResult GoogleLogin()
        {
            var redirectUrl = Url.Action(nameof(GoogleResponse), "Auth");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(GoogleDefaults.AuthenticationScheme, redirectUrl);
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet, Route("GoogleResponse")]
        public async Task<IActionResult> GoogleResponse()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null) return BadRequest();      
      
            var externalResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            if (!externalResult.Succeeded)
            {
                var email = info.Principal.FindFirstValue(ClaimValueTypes.Email);
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null) return BadRequest("You have to register your account");

                var result = await _userManager.AddLoginAsync(user, info);
                if (!result.Succeeded) return BadRequest("Something going wrong");
                
                await _signInManager.SignInAsync(user, false);
            }            
            return Redirect("https://localhost:44376/swagger/index.html");
        }

        private string? CreateCallBackUrl(string email, string token, string action)
            => Url.Action(action, "Auth", new { email, token }, Request.Scheme, Request.Host.Value);
    }
}