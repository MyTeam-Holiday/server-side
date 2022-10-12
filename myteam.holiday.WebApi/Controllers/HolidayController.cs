using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using myteam.holiday.Domain.Models;
using myteam.holiday.Domain.Services;

namespace myteam.holiday.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HolidayController : Controller
    {
        private readonly ILogger<HolidayController> _logger;
        private readonly IHolidayRepository _holidayRepository;

        public HolidayController(ILogger<HolidayController> logger, IHolidayRepository holidayRepository)
        {
            _logger = logger;
            _holidayRepository = holidayRepository;
        }

        [HttpGet, Route("GetHolidays")]
        public async Task<IEnumerable<Holiday>> GetHolidays(){
            return await _holidayRepository.GetHolidays();
        }
        [HttpGet, Route("GetHolidayByName")]
        public async Task<Holiday> GetHolidayByName(string holidayName){
            return await _holidayRepository.GetHolidayByName(holidayName);
        }
        [HttpPost, Route("CreateHoliday")]
        public async Task<int> CreateHoliday(Holiday holiday, string teamGuId){
            return await _holidayRepository.CreateHoliday(holiday, teamGuId);
        }
        [HttpDelete, Route("DeleteHoliday")]
        public async Task<bool> DeleteHoliday(string guId){
            return await _holidayRepository.DeleteHoliday(guId);
        }
    }
}