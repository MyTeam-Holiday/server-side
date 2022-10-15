using Microsoft.EntityFrameworkCore;
using myteam.holiday.Domain.Models;
using myteam.holiday.Domain.Services;
using myteam.holiday.EntityFramework.Data;

namespace myteam.holiday.EntityFramework.Services
{
    public class GenericAppDbService<T> : IGenericAppDbService<T> where T : UserGuId
    {
        private readonly AppDbContextFactory? _contextFactory;

        public GenericAppDbService(AppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
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
                T? entity = await context.Set<T>().FirstOrDefaultAsync((x) => x.GuId == id);

                return entity!;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            using (AppDbContext context = _contextFactory!.CreateDbContext())
            {
                var removableEntity = await context.Set<T>().FirstOrDefaultAsync(x => x.GuId == id);
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
                entity.GuId = id;

                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }

        }
    }
}
