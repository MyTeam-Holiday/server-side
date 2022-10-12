using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myteam.holiday.Domain.Models;

namespace myteam.holiday.Domain.Services
{
    public interface ITeamRepository
    {
        Task<int> CreateTeam(Team team, string userGuId);
        Task<bool> DeleteTeam(string guId);
        Task<Team> GetTeamByName(string teamName);
        Task<bool> UpdateTeam(Team team);
        Task<bool> AppendUser(string userGuId, string teamGuId);
        Task<bool> DeleteUser(string userGuId, string teamGuId);
        Task<bool> SetRoleToUser(string userGuId, string teamGuId, string userRoleGuId);
        Task<IEnumerable<Team>> GetTeams();
    }
}