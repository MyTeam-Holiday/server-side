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
    public class TeamRepository : ITeamRepository
    {
        private readonly AppDbContextFactory _contextFactory;
        public TeamRepository(AppDbContextFactory contextFactory){
            _contextFactory = contextFactory;
        }
        public async Task<bool> AppendUser(string userGuId, string teamGuId)
        {
            using (AppDbContext _context = _contextFactory.CreateDbContext()){
                var userGuIdParam = new MySqlParameter("@userGuId", userGuId);
                var teamGuIdParam = new MySqlParameter("@teamGuId", teamGuId);

                await _context.Database.ExecuteSqlRawAsync(
                    "CALL th_AppendUserFromTeam(@userGuId, @teamGuId)",
                    userGuIdParam,
                    teamGuIdParam
                );

                return true;
            }
            
        }

        public async Task<int> CreateTeam(Team team, string userGuId)
        {
            using (AppDbContext _context = _contextFactory.CreateDbContext()){
                var userGuIdParam = new MySqlParameter("@userGuId", userGuId);
                var teamNameParam = new MySqlParameter("@teamName", team.TeamName);
                var baseGuidParam = new MySqlParameter("@baseGuid", team.BaseGuId);
                var passwordHashParam = new MySqlParameter("@passwordHash", team.PasswordHash);
                var passwordSaltParam = new MySqlParameter("@passwordSalt", team.PasswordSalt);
                var hasPrivateParam = new MySqlParameter("@hasPrivate", team.HasPrivate);
                var onlyInviteParam = new MySqlParameter("@onlyInvite", team.OnlyInvite);

                var result = await _context.Database.ExecuteSqlRawAsync(
                    "CALL th_CreateTeam(@userGuId, @teamName, @baseGuid, @passwordSalt, @passwordHash, @hasPrivate, @onlyInvite)",
                    userGuIdParam,
                    teamNameParam,
                    baseGuidParam,
                    passwordSaltParam,
                    passwordHashParam,
                    hasPrivateParam,
                    onlyInviteParam 
                );

                return result;
            }
            
        }

        public async Task<bool> DeleteTeam(string guId)
        {
            using (AppDbContext _context = _contextFactory.CreateDbContext()){
                var guIdParam = new MySqlParameter("@guId", guId);

                await _context.Database.ExecuteSqlRawAsync(
                    "CALL th_DeleteTeam(@guId);",
                    guIdParam
                );

                return true;
            }
            
        }

        public async Task<bool> DeleteUser(string userGuId, string teamGuId)
        {
            using (AppDbContext _context = _contextFactory.CreateDbContext()){
                var userGuIdParam = new MySqlParameter("@userGuId", userGuId);
                var teamGuIdParam = new MySqlParameter("@teamGuId", teamGuId);

                await _context.Database.ExecuteSqlRawAsync(
                    "CALL th_DeleteUserFromTeam(@teamGuid, @userGuId)",
                    teamGuIdParam,
                    userGuIdParam
                    
                );

                return true;
            }
            
        }

        public async Task<Team> GetTeamByName(string teamName)
        {
            using (AppDbContext _context = _contextFactory.CreateDbContext()){
                var teamNameParam = new MySqlParameter("@teamName", teamName);

                var result = await _context.Team.FromSqlRaw(
                    "CALL th_GetTeamByName(@teamName)",
                    teamNameParam
                )
                .ToListAsync();

                var team = new Team(){
                    GuId = result.First().GuId,
                    TeamName = result.First().TeamName,
                    BaseGuId = result.First().BaseGuId,
                    PasswordHash = result.First().PasswordHash,
                    PasswordSalt = result.First().PasswordSalt,
                    InviteLink = result.First().InviteLink,
                    HasPrivate = result.First().HasPrivate,
                    OnlyInvite = result.First().OnlyInvite
                };

                return team;
            }
            
        }

        public async Task<IEnumerable<Team>> GetTeams()
        {
            using (AppDbContext _context = _contextFactory.CreateDbContext()){
                var result = await _context.Team.FromSqlRaw<Team>(
                "CALL th_GetTeams();"
                )
                .ToListAsync();

                return result;
            }
            
        }

        public async Task<bool> SetRoleToUser(string userGuId, string teamGuId, string userRoleGuId)
        {
            using (AppDbContext _context = _contextFactory.CreateDbContext()){
                var userGuIdParam = new MySqlParameter("@userGuId", userGuId);
                var teamGuIdParam = new MySqlParameter("@teamGuId", teamGuId);
                var userRoleGuIdParam = new MySqlParameter("@userRoleGuId", userRoleGuId);

                await _context.Database.ExecuteSqlRawAsync(
                    "CALL th_SetRoleToUser(@userGuId, @teamGuId, @userRoleGuid);",
                    userGuIdParam,
                    teamGuIdParam,
                    userRoleGuIdParam
                );

                return true;
            }
            
        }

        public async Task<bool> UpdateTeam(Team team)
        {
            using (AppDbContext _context = _contextFactory.CreateDbContext()){
                var teamNameParam = new MySqlParameter("@teamName", team.TeamName);
                var baseGuidParam = new MySqlParameter("@baseGuid", team.BaseGuId);
                var passwordHashParam = new MySqlParameter("@passwordHash", team.PasswordHash);
                var passwordSaltParam = new MySqlParameter("@passwordSalt", team.PasswordSalt);
                var hasPrivateParam = new MySqlParameter("@hasPrivate", team.HasPrivate);
                var onlyInviteParam = new MySqlParameter("@onlyInvite", team.OnlyInvite);

                var result = await _context.Database.ExecuteSqlRawAsync(
                    "CALL th_UpdateTeam(@teamName, @baseGuid, @passwordSalt, @passwordHash, @hasPrivate, @OnlyInvite",
                    teamNameParam,
                    baseGuidParam,
                    passwordSaltParam,
                    passwordHashParam,
                    hasPrivateParam,
                    onlyInviteParam 
                );

                return true;
            }
            
        }
    }
}