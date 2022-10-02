using Microsoft.EntityFrameworkCore;
using myteam.holiday.Domain.Models;
using myteam.holiday.EntityFramework.Data;
using myteam.holiday.WebServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myteam.holiday.EntityFramework.Services
{
    public class GenericAppDbService<T> where T: DefaultObject
    {
        private readonly AppDbContextFactory? _contextFactory;
        public GenericAppDbService(AppDbContextFactory? context)
        {
            _contextFactory = context;
        }

        public async Task<T> Create(T entity)
        {
            using (AppDbContext context = _contextFactory!.CreateDbContext())
            {
                var result = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return result.Entity;
            }
        }

        public async Task<T> GetValue(Guid id)
        {
            using (AppDbContext context = _contextFactory!.CreateDbContext())
            {
                T? entity = await context.Set<T>().FirstOrDefaultAsync((x) => x.Id == id);

                return entity!;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            using (AppDbContext context = _contextFactory!.CreateDbContext())
            {
                var removableEntity = await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
                context.Set<T>().Remove(removableEntity!);
                await context.SaveChangesAsync();

                return true;
            }
        }
        public async Task<IEnumerable<T>> GetAllValues()
        {
            using (AppDbContext context = _contextFactory!.CreateDbContext())
            {
                IEnumerable<T>? entities = await context.Set<T>().ToListAsync();

                return entities!;
            }
        }

        public async Task<T> Update(Guid id, T entity)
        {
            using (AppDbContext context = _contextFactory!.CreateDbContext())
            {
                entity.Id = id;

                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
            
        }
    }
}
