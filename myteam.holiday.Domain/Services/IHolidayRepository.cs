using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myteam.holiday.Domain.Models;

namespace myteam.holiday.Domain.Services
{
    public interface IHolidayRepository
    {
        Task<int> CreateHoliday(Holiday holiday, string teamGuId);
        Task<bool> DeleteHoliday(string guId);
        Task<Holiday> GetHolidayByName(string holidayName);
        Task<IEnumerable<Holiday>> GetHolidays();
    }
}