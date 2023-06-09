using System.Linq.Expressions;

namespace Application.Interfaces.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(Guid id);
        Task<T> CreateAsync(T entity);

        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid Id);
    }
}

