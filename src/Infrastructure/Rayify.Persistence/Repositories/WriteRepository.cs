using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Rayify.Application.Repositories;
using Rayify.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rayify.Persistence.Contexts;

namespace Rayify.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly RayifyDbContext _context;

        public WriteRepository(RayifyDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> values)
        {
            await Table.AddRangeAsync(values);
            return true;
        }

        public bool Remove(T entity)
        {
            EntityEntry<T> entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveById(string id)
        {
            T entity = await Table.FindAsync(Guid.Parse(id));
            return Remove(entity);
        }

        public bool RemoveRange(List<T> values)
        {
            Table.RemoveRange(values);
            return true;
        }

        public bool Update(T entity)
        {
            EntityEntry<T> entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

        public bool UpdateRange(List<T> values)
        {
            Table.UpdateRange(values);
            return true;
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();

    }
}
