﻿using System.Linq.Expressions;

namespace StudentManagementSystem.Repositories.Common
{
    public interface IBaseRepository<T> where T : class
    {
        void Create(T entity);
        IEnumerable<T> GetAll();
        IEnumerable<T> Getby(Expression<Func<T, bool>> expression);
        void Update(T entity);
        void Delete(T entity);


    }
}
