using GymHub.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHub.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly GymHubDbContext context;
        private readonly DbSet<T> dbSet;

        public Repository(GymHubDbContext _context)
        {
            context = _context;
            dbSet = context.Set<T>();
        }


        public async Task AddAsync(T entity)
        {
           await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
                await context.SaveChangesAsync();
            }

        }

        

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return  await dbSet.ToListAsync();
        }

        public IEnumerable<T> GetAllAttached()
        {
            return dbSet.AsQueryable();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

      

        public async Task UpdateAsync(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();

        }
    }
}
