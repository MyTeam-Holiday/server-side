using Microsoft.EntityFrameworkCore;
using myteam.holiday.WebServer.Data;
using myteam.holiday.WebServer.Model;

namespace myteam.holiday.WebServer.Services
{
    public class AppDbUserService
    {
        private readonly AppDbContext _context;
        public AppDbUserService(IDbContextFactory<AppDbContext> factoryContext)
        {
            _context = factoryContext.CreateDbContext();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User?> GetOneIdAsync(Guid userId)
        {
            return await _context.User.FirstOrDefaultAsync(user => user.Id == userId);
        }
        public async Task<User?> GetOneLoginAsync(string login)
        {
            return await _context.User.FirstOrDefaultAsync(user => user.Login == login);
        }

        public async Task<int> CreateUserAsync(User user)
        {
            user.Id = Guid.NewGuid();
            await _context.User.AddAsync(user);
            return await _context.SaveChangesAsync();
        }

        public async Task<User> UpdateUserAsync(User newUser)
        {
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
