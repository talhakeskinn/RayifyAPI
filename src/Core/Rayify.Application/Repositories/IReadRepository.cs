using Rayify.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rayify.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool tracking = true); // Eğer sorgu üzerinde çalışacaksak IQueryable kullanırız. List ve IEnumerable veriyi inmemory kısmına çeker ve orada işlem yapmanı sağlar. IQueryable ile çalışmak tercihimiz kodumuzun daha optimize olmasını istememiz.
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true); // burada efcore kısmında where sorgusunun çalışma koşullarına göre bir tanımlama yapıyoruz where kullanırken Where(c => c.id == id) gibi kullanıyoruz dolayısıyla Where'in aldığı parametrenin tanımını GetWhere metodumuzda yapıyoruz.
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetByIdAsync(string id, bool tracking = true);
    }
}
