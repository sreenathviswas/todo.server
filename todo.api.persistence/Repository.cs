using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ToDo.Api.Persistence
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {

        public DbContext DbContext { get; private set; }

        public Repository(DbContext dbcontext)
        {
            DbContext = dbcontext;
        }

        protected virtual DbSet<T> Set
        {
            get { return DbContext.Set<T>(); }
        }

        protected virtual IQueryable<T> Queryable
        {
            get { return DbContext.Set<T>(); }
        }

        public virtual async Task<IList<T>> GetAllAsync()
        {
            return await Queryable.ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await Queryable.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public virtual async Task<T> SaveAsync(T entity)
        {
            if(DbContext.ChangeTracker.Entries().All(e =>e.Entity != entity))
            {
                Set.Add(entity);
            }

            await DbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<IList<T>> FindAsync(Expression<Func<T, bool>> criterion)
        {
            return await Queryable.Where(criterion).ToListAsync();
        }

        public virtual async Task RemoveAsync(T entity)
        {
            var loadedEntity = await GetAsync(entity.Id);
            Set.Remove(loadedEntity);
            await DbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
