using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace todo.api.persistence
{
    public interface IRepository<T> : IDisposable
    {
        Task<IList<T>> GetAllAsync();

        Task<T> GetAsync(int id);

        Task<T> SaveAsync(T model);

        Task RemoveAsync(T model);

        Task<IList<T>> FindAsync(Expression<Func<T, bool>> criterion);
    }
}
