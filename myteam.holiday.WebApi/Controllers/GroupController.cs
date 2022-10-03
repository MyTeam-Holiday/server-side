using Microsoft.AspNetCore.Mvc;
using myteam.holiday.WebServer.Model;
using myteam.holiday.WebServer.Services;

namespace myteam.holiday.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly AppDbGroupService _groupService;
        private readonly ILogger<GroupController> _logger;

        public GroupController(
            ILogger<GroupController> logger,
            AppDbGroupService groupService)
        {
            _logger = logger;
            _groupService = groupService;
        }

        [HttpGet("GetAllGroup")]
        public async Task<ActionResult<IEnumerable<Group>>> GetAllGroup()
        {
            return Ok(await _groupService.GetAllAsync());
        }

        [HttpGet("GetGroup")]
        public async Task<ActionResult<Group>> GetGroup(Guid groupId)
        {
            return Ok(await _groupService.GetOneAsync(groupId));
        }

        [HttpPost("CreateGroup")]
        public async Task<ActionResult<int>> CreateGroup(Group group)
        {
            return Ok(await _groupService.CreateGroupAsync(group));
        }

        [HttpPost("UpdateGroup")]
        public async Task<ActionResult<int>> UpdateGroup(Guid oldId,Group newGroup)
        {
            return Ok(await _groupService.UpdateGroupAsync(oldId, newGroup));
        }

        [HttpDelete("DeleteGroup")]
        public async Task<ActionResult<int>> DeleteGroup(Guid id)
        {
            Group group = await _groupService.GetOneAsync(id) ?? new();
            return Ok(await _groupService.DeleteGroupAsync(group));
        }
    }
}
