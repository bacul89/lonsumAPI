using ApiTemplate.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTemplate.Services.Base
{
    public interface IService
    {
    }

    public interface IEntityService<T> : IService
        where T : BaseEntity
    {
        Task Create(T entity);
        void Delete(T entity);
        Task<IList<T>> GetAll();
        Task Update(T entity);
    }
}