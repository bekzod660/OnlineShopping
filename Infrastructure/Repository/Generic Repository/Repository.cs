using Application.Abstraction;
using Application.Interfaces.Repository;
using System.Linq.Expressions;

namespace Infrastructure.Repository.Generic_Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IApplicationDbContext _productDb;

        public Repository(IApplicationDbContext catalogDb)
        {
            _productDb = catalogDb;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            _productDb.Set<T>().Add(entity);
            await _productDb.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> DeleteAsync(Guid Id)
        {
            var entity = _productDb.Set<T>().Find(Id);
            if (entity != null)
            {
                _productDb.Set<T>().Remove(entity);
                await _productDb.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public virtual Task<IQueryable<T?>> GetAsync(Expression<Func<T, bool>> expression)
        {
            var res = Task.FromResult(_productDb.Set<T?>().Where(expression));
            return res;
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            T? res = await Task.FromResult(_productDb.Set<T>().Find(id));
            return res;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            if (entity != null)
            {
                _productDb.Set<T>().Update(entity);
                await _productDb.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
