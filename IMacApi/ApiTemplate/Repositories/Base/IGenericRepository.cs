using ApiTemplate.Commons;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiTemplate.Repositories.Base
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        Task<IList<T>> GetAll();
        Task<IList<T>> FindBy(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        void AddRange(IEnumerable<T> entities);
        T Delete(T entity);
        void Edit(T entity);
        void Save();
    }
}
