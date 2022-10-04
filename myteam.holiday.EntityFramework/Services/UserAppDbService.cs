using Microsoft.EntityFrameworkCore;
using myteam.holiday.Domain.Models;
using myteam.holiday.EntityFramework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myteam.holiday.EntityFramework.Services
{
    public class UserAppDbService
    {
        private readonly AppDbContextFactory? _contextFactory = new AppDbContextFactory();

        public async Task<List<User>> GetAllAsync()
        {
            using AppDbContext context = _contextFactory!.CreateDbContext();
            return await context.User.ToListAsync();
        }

        public async Task<User?> GetOneEmailAsync(string email)
        {
            using AppDbContext context = _contextFactory!.CreateDbContext();
            return await context.User.FirstOrDefaultAsync(user => user.UserEmail == email);
        }

        public async Task<int> CreateUserAsync(User user)
        {
            using AppDbContext context = _contextFactory!.CreateDbContext();
            await context.User.AddAsync(user);
            return await context.SaveChangesAsync();
        }

        public async Task<User> UpdateUserAsync(User newUser)
        {
            using AppDbContext context = _contextFactory!.CreateDbContext();
            User user = await GetOneEmailAsync(newUser.UserEmail!) ?? new();
            //newUser.PasswordHash = user.PasswordHash не сделан так как браузер и телега
            //будет создавать хэш и передавать его в теле
            newUser.PasswordSalt = user.PasswordSalt; //Salt никогда не меняется

            context.User.Update(newUser);
            await context.SaveChangesAsync();
            return newUser;
        }

        public async Task<int> DeleteUserAsync(string email)
        {
            using AppDbContext context = _contextFactory!.CreateDbContext();
            User? user = await GetOneEmailAsync(email);
            context.User.Remove(user!);
            return await context.SaveChangesAsync();
        }

        public async Task<bool> IsConvergeUserHashAsync(string email, string hash)
        {
            User? user = await GetOneEmailAsync(email);
            return (
                user != null &&
                user.PasswordHash == hash);
        }
    }
}
