using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myteam.holiday.Domain.Models;
using myteam.holiday.EntityFramework.Services;

namespace myteam.holiday.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CelebrationController : Controller
    {
        private readonly GenericAppDbService<Celebration> _appDbService;
        private readonly ILogger<CelebrationController> _logger;

        public CelebrationController(GenericAppDbService<Celebration> appDbService, ILogger<CelebrationController> logger)
        {
            _appDbService = appDbService;
            _logger = logger;
        }
        [HttpGet("GetAllCelebrations")]
        public async Task<ActionResult<IEnumerable<Celebration>>> GetAllCelebrations()
        {
            return Ok(await _appDbService.GetAllValues());
        }

        [HttpGet("GetCelebration")]
        public async Task<ActionResult<Celebration>> GetTeam(Guid celebrationId)
        {
            return Ok(await _appDbService.GetValue(celebrationId));
        }

        [HttpPost("CreateCelebration")]
        public async Task<ActionResult<Celebration>> CreateCelebration(Celebration celebration)
        {
            return Ok(await _appDbService.Create(celebration));
        }

        [HttpPost("UpdateCelebration")]
        public async Task<ActionResult<Celebration>> UpdateCelebration(Guid id, Celebration updatedCelebration)
        {
            return Ok(await _appDbService.Update(id, updatedCelebration));
        }

        [HttpDelete("DeleteCelebration")]
        public async Task<ActionResult<bool>> DeleteCelebration(Guid id)
        {
            return Ok(await _appDbService.Delete(id));
        }
    }
}
