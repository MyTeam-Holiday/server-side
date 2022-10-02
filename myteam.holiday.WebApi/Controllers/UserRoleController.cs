using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myteam.holiday.Domain.Models;
using myteam.holiday.EntityFramework.Services;

namespace myteam.holiday.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserRoleController : Controller
    {
        private readonly GenericAppDbService<UserRole> _appDbService;
        private readonly ILogger<UserRoleController> _logger;

        public UserRoleController(
            ILogger<UserRoleController> logger,
            GenericAppDbService<UserRole> appDbService)
        {
            _logger = logger;
            _appDbService = appDbService;
        }

        [HttpGet("GetAllUserRoles")]
        public async Task<ActionResult<IEnumerable<UserRole>>> GetAllUserRoles()
        {
            return Ok(await _appDbService.GetAllValues());
        }

        [HttpGet("GetUserRole")]
        public async Task<ActionResult<UserRole>> GetUserRole(Guid userRoleId)
        {
            return Ok(await _appDbService.GetValue(userRoleId));
        }

        [HttpPost("CreateUserRole")]
        public async Task<ActionResult<UserRole>> CreateUser(UserRole userRole)
        {
            return Ok(await _appDbService.Create(userRole));
        }

        [HttpPost("UpdateUserRole")]
        public async Task<ActionResult<UserRole>> UpdateUserRole(Guid id, UserRole updatedUserRole)
        {
            return Ok(await _appDbService.Update(id, updatedUserRole));
        }

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult<bool>> DeleteUserRole(Guid id)
        {
            return Ok(await _appDbService.Delete(id));
        }
    }
}
