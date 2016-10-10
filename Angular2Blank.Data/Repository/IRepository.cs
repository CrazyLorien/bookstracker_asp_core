using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angular2Blank.Data.Entities;

namespace Angular2Blank.Data.Repository
{
    public interface IRepository<T> where T: BaseEntity
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        Task<IEnumerable<T>> AddAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entities);
        Task DeleteAsync(IEnumerable<T> entities);

        IQueryable<T> GetQuery();
        Task<T> GetById(int id);
    }
}
