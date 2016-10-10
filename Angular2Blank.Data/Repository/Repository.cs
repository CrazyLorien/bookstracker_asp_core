using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angular2Blank.Data.Context;
using Angular2Blank.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Angular2Blank.Data.Repository
{
    public class Repository<T>: IRepository<T> where T : BaseEntity
    {
        private readonly Angular2BlankContext _context;

        public Repository(Angular2BlankContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> AddAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task DeleteAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetQuery()
        {
            return _context.Set<T>();
        }

        public Task<T> GetById(int id)
        {
            return _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
