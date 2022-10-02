using Microsoft.EntityFrameworkCore;
using myteam.holiday.WebServer.Data;
using myteam.holiday.WebServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myteam.holiday.WebServer.Services
{
    public class AppDbUserService
    {
        private readonly AppDbContext _context;
        public AppDbUserService(IDbContextFactory<AppDbContext> factoryContext)
        {
            _context = factoryContext.CreateDbContext();
        }

        public async Task<int> CreateUserAsync(User user)
        {
            await _context.User.AddAsync(user);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User?> GetOneAsync(int userId)
        {
            return await _context.User.FirstOrDefaultAsync(user => user.Id == userId);
        }

        public async Task<User> UpdateUserAsync(int oldId, User newUser)
        {
            newUser.Id = oldId;
            _context.User.Update(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<int> DeleteUserAsync(User user)
        {
            _context.User.Remove(user);
            return await _context.SaveChangesAsync();
        }
    }
}
