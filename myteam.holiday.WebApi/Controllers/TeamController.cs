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
    public class TeamController : Controller
    {
        private readonly ILogger<TeamController> _logger;
        private readonly ITeamRepository _teamRepository;

        public TeamController(ILogger<TeamController> logger, ITeamRepository teamRepository)
        {
            _logger = logger;
            _teamRepository = teamRepository;
        }

        [HttpGet, Route("GetTeams")]
        public async Task<IEnumerable<Team>> GetTeams(){
            return await _teamRepository.GetTeams();
        }
        [HttpGet, Route("GetTeamByName")]
        public async Task<Team> GetTeamByName(string teamName){
            return await _teamRepository.GetTeamByName(teamName);
        }
        [HttpPost, Route("AppendUser")]
        public async Task<bool> AppendUser(string userGuId, string teamGuId){
            return await _teamRepository.AppendUser(userGuId, teamGuId);
        }
        [HttpPost, Route("CreateTeam")]
        public async Task<int> CreateTeam(Team team, string userGuId){
            return await _teamRepository.CreateTeam(team, userGuId);
        }
        [HttpDelete, Route("DeleteTeam")]
        public async Task<bool> DeleteTeam(string guId){
            return await _teamRepository.DeleteTeam(guId);
        }
        [HttpDelete, Route("DeleteUser")]
        public async Task<bool> DeleteUser(string userGuId, string teamGuId){
            return await _teamRepository.DeleteUser(userGuId, teamGuId);
        }
        [HttpPut, Route("UpdateTeam")]
        public async Task<bool> UpdateTeam(Team team){
            return await _teamRepository.UpdateTeam(team);
        }
        [HttpPut, Route("SetRoleToUser")]
        public async Task<bool> SetRoleToUser(string userGuId, string teamGuId, string userRoleGuId){
            return await _teamRepository.SetRoleToUser(userGuId, teamGuId, userRoleGuId);
        }
    }
}