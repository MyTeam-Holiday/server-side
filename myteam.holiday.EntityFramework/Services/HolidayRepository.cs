using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using myteam.holiday.Domain.Models;
using myteam.holiday.Domain.Services;
using myteam.holiday.EntityFramework.Data;

namespace myteam.holiday.EntityFramework.Services
{
    public class HolidayRepository : IHolidayRepository
    {
        private readonly AppDbContextFactory _context;
        public HolidayRepository(AppDbContextFactory context){
            _context = context;
        }
        public async Task<int> CreateHoliday(Holiday holiday, string teamGuId)
        {
            using (AppDbContext context = _context.CreateDbContext()){
                var holidayNameParam = new MySqlParameter("@holidayName", holiday.HolidayName);
                var holidayStatusParam = new MySqlParameter("@holidayStatus", holiday.HolidayStatus);
                var holidayTimeStampParam = new MySqlParameter("@holidayTimeStamp", holiday.HolidayTimeStamp);
                var userGuIdParam = new MySqlParameter("@userGuId", holiday.UserGuId);
                var teamGuIdParam = new MySqlParameter("@teamGuId", teamGuId);

                var result = await context.Database.ExecuteSqlRawAsync(
                    "CALL th_CreateHoliday(@holidayName, @holidayStatus, @holidayTimeStamp, @teamGuId, @userGuId)",
                    holidayNameParam,
                    holidayStatusParam,
                    holidayTimeStampParam,
                    teamGuIdParam,
                    userGuIdParam
                );

                return result;
            }
            
        }

        public async Task<bool> DeleteHoliday(string guId)
        {
            using (AppDbContext context = _context.CreateDbContext()){
                var holidayGuIdParam = new MySqlParameter("@GuId", guId);

                await context.Database.ExecuteSqlRawAsync(
                    "CALL th_DeleteHoliday(@GuId)",
                    holidayGuIdParam
                );

                return true;
            }
           
        }

        public async Task<Holiday> GetHolidayByName(string holidayName)
        {
            using (AppDbContext context = _context.CreateDbContext()){
                var holidayNameParam = new MySqlParameter("@holidayName", holidayName);

                var result = await context.Holiday.FromSqlRaw(
                    "CALL th_GetHolidayByName(@holidayName)",
                    holidayNameParam
                ).ToListAsync();

                var holiday = new Holiday(){
                    GuId = result.First().GuId,
                    HolidayName = result.First().HolidayName,
                    HolidayStatus = result.First().HolidayStatus,
                    HolidayTimeStamp = result.First().HolidayTimeStamp,
                    UserGuId = result.First().UserGuId
                };

                return holiday;
            }
            
        }

        public async Task<IEnumerable<Holiday>> GetHolidays()
        {
            using (AppDbContext context = _context.CreateDbContext()){
                var result = await context.Holiday.FromSqlRaw<Holiday>(
                "CALL th_GetHolidays();"
                ).ToListAsync();

            return result;
            }
            
        }

    }
}