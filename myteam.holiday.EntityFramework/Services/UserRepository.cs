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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContextFactory _contextFactory;
        public UserRepository(AppDbContextFactory contextFactory){
            _contextFactory = contextFactory;
        }
        public async Task<string> PreCreateUser(string userName, string userEmail)
        {
            using (AppDbContext _context = _contextFactory.CreateDbContext()){
                var userNameParam = new MySqlParameter("@userName", userName);
                var userEmailParam = new MySqlParameter("@userEmail", userEmail);

                var result =  await _context.UserGuId.FromSqlRaw<UserGuId>(
                    "CALL th_PreCreateUser(@userName, @userEmail);",
                    userNameParam,
                    userEmailParam
                ).ToListAsync();


                return result.First().GuId.ToString();
            }
            
        }
        public async Task<int> CreateUser(User user)
        {
            using (AppDbContext _context  = _contextFactory.CreateDbContext()){
                var userGuIdParam = new MySqlParameter("@GuId", user.GuId);
                var userPasswordHash = new MySqlParameter("@PasswordHash", user.PasswordHash);
                var userPasswordSalt = new MySqlParameter("@PasswordSalt", user.PasswordSalt);

                var result = await _context.Database.ExecuteSqlRawAsync(
                    "CALL th_CreateUser(@GuId, @PasswordHash, @PasswordSalt)",
                    userGuIdParam,
                    userPasswordHash,
                    userPasswordSalt
                );

                return result;
            }
            
        }
        public async Task<User> GetUserByEmail(string email)
        {
            using (AppDbContext _context = _contextFactory.CreateDbContext()){
                var userEmailParam = new MySqlParameter("@Email", email);

                var result = await _context.User.FromSqlRaw<User>(
                    "CALL th_GetUserByEmail(@Email);",
                    userEmailParam
                ).ToListAsync();

                var user = new User(){
                    GuId = result.First().GuId,
                    Username = result.First().Username,
                    UserEmail = result.First().UserEmail,
                    PasswordHash = result.First().PasswordHash,
                    PasswordSalt = result.First().PasswordSalt,
                    HasVerified = result.First().HasVerified
                };

                return user;
            }
            
        }
        public async Task<User> GetUserByGuId(string guId)
        {
            using (AppDbContext _context = _contextFactory.CreateDbContext()){
                var userGuIdParam = new MySqlParameter("@GuId", guId);

                var result = await _context.User.FromSqlRaw<User>(
                    "CALL th_GetUserByGuId(@GuId);",
                    userGuIdParam
                ).ToListAsync();

                var user = new User(){
                    GuId = result.First().GuId,
                    Username = result.First().Username,
                    UserEmail = result.First().UserEmail,
                    PasswordHash = result.First().PasswordHash,
                    PasswordSalt = result.First().PasswordSalt,
                    HasVerified = result.First().HasVerified
                };

                return user;
            }
            
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            using (AppDbContext _context = _contextFactory.CreateDbContext()){
                var result = await _context.User.FromSqlRaw<User>(
                    "CALL th_GetUsers()"
                ).ToListAsync();

                return result;
            }
           
        }
        public async Task<bool> UpdateUser(User user)
        {
            using (AppDbContext _context = _contextFactory.CreateDbContext()){
                var userGuIdParam = new MySqlParameter("@GuId", user.GuId);
                var userNameParam = new MySqlParameter("@userName", user.Username);
                var userEmailParam = new MySqlParameter("@userEmail", user.UserEmail);
                var userPasswordHash = new MySqlParameter("@PasswordHash", user.PasswordHash);
                var userPasswordSalt = new MySqlParameter("@PasswordSalt", user.PasswordSalt);

                await _context.Database.ExecuteSqlRawAsync(
                    "CALL th_UpdateUser(@GuId, @userName, @userEmail, @PasswordHash, @PasswordSalt)",
                    userGuIdParam,
                    userNameParam,
                    userEmailParam,
                    userPasswordHash,
                    userPasswordSalt
                );

                return true;
            }
            
        }
        public async Task<bool> DeleteUser(string guId)
        {
            using (AppDbContext _context = _contextFactory.CreateDbContext()){
                var userGuIdParam = new MySqlParameter("@GuId", guId);

                await _context.Database.ExecuteSqlRawAsync(
                    "CALL th_DeleteUser(@GuId)",
                    userGuIdParam
                );

                return true;
            }
            
        }
    }
}