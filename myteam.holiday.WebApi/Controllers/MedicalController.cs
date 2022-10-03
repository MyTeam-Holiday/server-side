using Microsoft.AspNetCore.Mvc;
using myteam.holiday.Domain.Models;
using myteam.holiday.Domain.Services;
using myteam.holiday.EntityFramework.Services;

namespace myteam.holiday.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicalController : Controller
    {
        private readonly IGenericAppDbService<Medical> _appDbService;
        private readonly ILogger<CelebrationController> _logger;
        public MedicalController(IGenericAppDbService<Medical> appDbService, ILogger<CelebrationController> logger)
        {
            _appDbService = appDbService;
            _logger = logger;
        }
        [HttpGet("GetAllMedicals")]
        public async Task<ActionResult<IEnumerable<Medical>>> GetAllMedicals()
        {
            return Ok(await _appDbService.GetAllValues());
        }

        [HttpGet("GetMedical")]
        public async Task<ActionResult<Medical>> GetMedical(Guid medicalId)
        {
            return Ok(await _appDbService.GetValue(medicalId));
        }

        [HttpPost("CreateMedical")]
        public async Task<ActionResult<Medical>> CreateCelebration(Medical medical)
        {
            return Ok(await _appDbService.Create(medical));
        }

        [HttpPost("UpdateMedical")]
        public async Task<ActionResult<Medical>> UpdateMedical(Guid id, Medical updatedMedical)
        {
            return Ok(await _appDbService.Update(id, updatedMedical));
        }

        [HttpDelete("DeleteMedical")]
        public async Task<ActionResult<bool>> DeleteMedical(Guid id)
        {
            return Ok(await _appDbService.Delete(id));
        }
    }
}
