using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using myteam.holiday.WebApi.Services;
using myteam.holiday.WebServer.Model;
using myteam.holiday.WebServer.Services;
using System.Security.Cryptography;
using System.Text;

namespace myteam.holiday.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbUserService _userService;
        private readonly DbValidationService _validationService;
        private readonly ILogger<UserController> _logger;

        public UserController(
            AppDbUserService userService,
            DbValidationService validationService,
            ILogger<UserController> logger)
        {
            _logger = logger;
            _userService = userService;
            _validationService = validationService;
        }

        [HttpGet("GetAllUser")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUser()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [HttpGet("GetUser")]
        public async Task<ActionResult<User>> GetUser(string login, string hash)
        {
            if(await _validationService.IsConvergeUserPasswords(login, hash))
            {
                User user = await _userService.GetOneLoginAsync(login) ?? new();
                user.Salt = default;
                return Ok(user);
            }
            return BadRequest("Ошибка: некорректные данные модели User");
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<string>> CreateUser(User user, string password)
        {
            if(await _validationService.IsValidCreateUserModelAsync(user))
            {
                byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));
                user.Hash = hashed;
                user.Salt = Encoding.UTF8.GetString(salt);
                user.RoleId = 1;
                await _userService.CreateUserAsync(user);
                return Ok(hashed);
            }
            return BadRequest("Ошибка: некорректные данные модели User");
        }

        [HttpPost("UpdateUser")]
        public async Task<ActionResult<int>> UpdateUser(User newUser, string hash)
        {
            if(await _validationService.IsValidUpdateUserModelAsync(newUser, hash))
            {
                return Ok(await _userService.UpdateUserAsync(newUser));
            }
            return BadRequest("Ошибка: некорректные данные модели User");
        }

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult<int>> DeleteUser(string login, string hash)
        {
            if (await _validationService.IsConvergeUserPasswords(login, hash))
            {
                User user = await _userService.GetOneLoginAsync(login) ?? new();
                return Ok(await _userService.DeleteUserAsync(user));
            }
            return BadRequest("Ошибка: некорректные данные модели User");
        }
    }
}
