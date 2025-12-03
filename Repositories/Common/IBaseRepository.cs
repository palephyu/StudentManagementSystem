using System.Linq.Expressions;

namespace StudentManagementSystem.Repositories.Common
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression);

        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }

}

