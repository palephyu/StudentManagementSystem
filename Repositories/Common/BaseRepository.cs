using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DAO;
using System.Linq.Expressions;

namespace StudentManagementSystem.Repositories.Common
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly StudentdbContext _studentdbContext;
        private readonly DbSet<T> _dbSet;
        public BaseRepository(StudentdbContext studentdbContext)
        {
            _studentdbContext = studentdbContext;
            _dbSet = _studentdbContext.Set<T>();
        }

        public void Create(T entity)
        {
            _studentdbContext.Add<T>(entity);
        }

        public void Delete(T entity)
        {
            _studentdbContext.Update<T>(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsNoTracking().AsEnumerable();
        }

        public IEnumerable<T> Getby(Expression<Func<T, bool>> expression)
        {
            return _dbSet.AsNoTracking().Where(expression).AsEnumerable();
        }

        public void Update(T entity)
        {
            _studentdbContext.Update<T>(entity);
        }
    }
}
