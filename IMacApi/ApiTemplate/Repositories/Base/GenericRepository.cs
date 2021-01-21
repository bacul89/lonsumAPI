using ApiTemplate.Commons;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTemplate.Repositories.Base
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
        where T : BaseEntity
    {
        protected DbContext _entities;
        protected readonly DbSet<T> _dbset;
        
        public GenericRepository(DbContext context)
        {
            _entities = context;
            _dbset = context.Set<T>();
        }

        public virtual async Task<IList<T>> GetAll()
        {
            return await _dbset.ToListAsync<T>();
        }

        public async Task<IList<T>> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var query =  await _dbset.Where(predicate).ToListAsync();
            return query;
        }

        public virtual T Add(T entity)
        {
            return _dbset.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _entities.Set<T>().AddRange(entities);
        }

        public virtual T Delete(T entity)
        {
            return _dbset.Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            _entities.SaveChangesAsync();
        }
    }
}
