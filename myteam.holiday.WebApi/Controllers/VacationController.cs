using Microsoft.AspNetCore.Mvc;
using myteam.holiday.Domain.Models;
using myteam.holiday.EntityFramework.Services;

namespace myteam.holiday.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VacationController : Controller
    {
        private readonly GenericAppDbService<Vacation> _appDbService;
        private readonly ILogger<VacationController> _logger;

        public VacationController(GenericAppDbService<Vacation> appDbService, ILogger<VacationController> logger)
        {
            _appDbService = appDbService;
            _logger = logger;
        }

        [HttpGet("GetAllVacations")]
        public async Task<ActionResult<IEnumerable<Vacation>>> GetAllVacations()
        {
            return Ok(await _appDbService.GetAllValues());
        }

        [HttpGet("GetVacation")]
        public async Task<ActionResult<Vacation>> GetVacation(Guid vacationId)
        {
            return Ok(await _appDbService.GetValue(vacationId));
        }

        [HttpPost("CreateVacation")]
        public async Task<ActionResult<Vacation>> CreateVacation(Vacation vacation)
        {
            return Ok(await _appDbService.Create(vacation));
        }

        [HttpPost("UpdateVacation")]
        public async Task<ActionResult<Vacation>> UpdateVacation(Guid id, Vacation updatedVacation)
        {
            return Ok(await _appDbService.Update(id, updatedVacation));
        }

        [HttpDelete("DeleteVacation")]
        public async Task<ActionResult<bool>> DeleteVacation(Guid id)
        {
            return Ok(await _appDbService.Delete(id));
        }
    }
}
