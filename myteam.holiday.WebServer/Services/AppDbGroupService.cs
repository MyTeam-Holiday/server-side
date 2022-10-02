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
    public class AppDbGroupService
    {
        private readonly AppDbContext _context;
        public AppDbGroupService(IDbContextFactory<AppDbContext> factoryContext)
        {
            _context = factoryContext.CreateDbContext();
        }

        public async Task<int> CreateGroupAsync(Group group)
        {
            await _context.Group.AddAsync(group);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await _context.Group.ToListAsync();
        }

        public async Task<Group?> GetOneAsync(int groupId)
        {
            return await _context.Group.FirstOrDefaultAsync(group => group.Id == groupId);
        }

        public async Task<Group> UpdateGroupAsync(int oldId, Group newGroup)
        {
            newGroup.Id = oldId;
            _context.Group.Update(newGroup);
            await _context.SaveChangesAsync();
            return newGroup;
        }

        public async Task<int> DeleteGroupAsync(Group group)
        {
            _context.Group.Remove(group);
            return await _context.SaveChangesAsync();
        }
    }
}
