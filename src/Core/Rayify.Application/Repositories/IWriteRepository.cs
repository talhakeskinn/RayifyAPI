using Rayify.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayify.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> values);
        bool Remove(T entity);
        Task<bool> RemoveById(string id);
        bool RemoveRange(List<T> values);
        bool Update(T entity);
        bool UpdateRange(List<T> values);
        Task<int> SaveAsync();
    }
}
