using Microsoft.AspNetCore.Mvc;
using myteam.holiday.Domain.Models;
using myteam.holiday.Domain.Services;
using myteam.holiday.EntityFramework.Data;
using myteam.holiday.EntityFramework.Services;

namespace myteam.holiday.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly IGenericAppDbService<Team> _appDbService;
        private readonly ILogger<TeamController> _logger;

        public TeamController(
            ILogger<TeamController> logger,
            IGenericAppDbService<Team> appDbService)
        {
            _logger = logger;
            _appDbService = appDbService;
        }

        [HttpGet("GetAllTeams")]
        public async Task<ActionResult<IEnumerable<Team>>> GetAllTeams()
        {
            return Ok(await _appDbService.GetAllValues());
        }

        [HttpGet("GetTeam")]
        public async Task<ActionResult<Team>> GetTeam(Guid teamId)
        {
            return Ok(await _appDbService.GetValue(teamId));
        }

        [HttpPost("CreateTeam")]
        public async Task<ActionResult<Team>> CreateTeam(Team team)
        {
            return Ok(await _appDbService.Create(team));
        }

        [HttpPost("UpdateTeam")]
        public async Task<ActionResult<Team>> UpdateTeam(Guid id, Team updatedTeam)
        {
            return Ok(await _appDbService.Update(id, updatedTeam));
        }

        [HttpDelete("DeleteTeam")]
        public async Task<ActionResult<bool>> DeleteTeam(Guid id)
        {
            return Ok(await _appDbService.Delete(id));
        }
    }
}
