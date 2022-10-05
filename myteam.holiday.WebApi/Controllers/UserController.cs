using Microsoft.AspNetCore.Mvc;
using myteam.holiday.Domain.Models;
using myteam.holiday.EntityFramework.Services;
using myteam.holiday.Domain.Services;
using myteam.holiday.WebApi.Services;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace myteam.holiday.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserAppDbService _appDbService;
        private readonly ILogger<UserController> _logger;
        private readonly ModelValidationService _validationService;

        public UserController(
            ILogger<UserController> logger,
            UserAppDbService appDbService,
            ModelValidationService validationService)
        {
            _logger = logger;
            _appDbService = appDbService;
            _validationService = validationService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return Ok(await _appDbService.GetAllAsync());
        }

        [HttpGet("GetUserToJoinAccount")]
        public async Task<ActionResult<User>> GetUserToJoinAccount(string email, string password)
        {
            if (await _validationService.IsCanJoinUserInAccount(email, password))
            {
                User? user = await _appDbService.GetOneEmailAsync(email) ?? new();
                user.PasswordSalt = default;
                return Ok(user);
            }
            return BadRequest("Ошибка: некорректные данные модели User");
        }

        [HttpGet("GetUser")]
        public async Task<ActionResult<User>> GetUser(string email, string hash)
        {
            if (await _appDbService.IsConvergeUserHashAsync(email, hash))
            {
                User user = await _appDbService.GetOneEmailAsync(email) ?? new();
                user.PasswordSalt = default;
                return Ok(user);
            }
            return BadRequest("Ошибка: некорректные данные модели User");
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<string>> CreateUser(User user, string password)
        {
            if (await _validationService.IsCanCreateUserAccount(user))
            {
                byte[] salt = RandomNumberGenerator.GetBytes(16);
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));
                user.PasswordHash = hashed;
                user.PasswordSalt = Encoding.Unicode.GetString(salt);
                user.Id = Guid.NewGuid();
                user.UserRole!.Id = Guid.NewGuid();
                await _appDbService.CreateUserAsync(user);
                return Ok(hashed);
            }
            return BadRequest("Ошибка: некорректные данные модели User");
        }

        [HttpPost("UpdateUser")]
        public async Task<ActionResult<User>> UpdateUser(User newUser, string hash)
        {
            if (await _validationService.IsCanUpdateUserAccount(newUser, hash))
            {
                await _appDbService.UpdateUserAsync(newUser);
                return newUser;
            }
            return BadRequest("Ошибка: некорректные данные модели User");
        }

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult<User>> DeleteUser(string email, string hash)
        {
            if (await _appDbService.IsConvergeUserHashAsync(email, hash))
            {
                return Ok(await _appDbService.DeleteUserAsync(email));
            }
            return BadRequest("Ошибка: некорректные данные модели User");
        }
    }
}
