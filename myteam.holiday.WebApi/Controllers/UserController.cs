using Microsoft.AspNetCore.Mvc;
using myteam.holiday.Domain.Models;
using myteam.holiday.EntityFramework.Services;
using myteam.holiday.Domain.Services;

namespace myteam.holiday.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IGenericAppDbService<User> _appDbService;
        private readonly ILogger<UserController> _logger;

        public UserController(
            ILogger<UserController> logger,
            IGenericAppDbService<User> appDbService)
        {
            _logger = logger;
            _appDbService = appDbService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return Ok(await _appDbService.GetAllValues());
        }

        [HttpGet("GetUser")]
        public async Task<ActionResult<User>> GetUser(Guid userId)
        {
            return Ok(await _appDbService.GetValue(userId));
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            return Ok(await _appDbService.Create(user));
        }

        [HttpPost("UpdateUser")]
        public async Task<ActionResult<User>> UpdateUser(Guid id, User updatedUser)
        {
            return Ok(await _appDbService.Update(id, updatedUser));
        }

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult<bool>> DeleteUser(Guid id)
        {
            return Ok(await _appDbService.Delete(id));
        }
    }
}
