using Microsoft.AspNetCore.Mvc;
using myteam.holiday.WebServer.Model;
using myteam.holiday.WebServer.Services;

namespace myteam.holiday.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(
            ILogger<UserController> logger,
            AppDbUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("GetAllUser")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUser()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [HttpGet("GetUser")]
        public async Task<ActionResult<User>> GetUser(int userId)
        {
            return Ok(await _userService.GetOneAsync(userId));
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<int>> CreateUser(User user)
        {
            return Ok(await _userService.CreateUserAsync(user));
        }

        [HttpPost("UpdateUser")]
        public async Task<ActionResult<int>> UpdateUser(int oldId, User newUser)
        {
            return Ok(await _userService.UpdateUserAsync(oldId, newUser));
        }

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult<int>> DeleteUser(int id)
        {
            User user = await _userService.GetOneAsync(id) ?? new();
            return Ok(await _userService.DeleteUserAsync(user));
        }
    }
}
