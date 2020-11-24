using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Layer.Repository
{
    interface IDataRepository<T> where T : class
    {
        T Find(Expression<Func<T, bool>> predicate);
        ICollection<T> FindAll(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate);
        T Get(int id);
        IQueryable<T> GetAll();
        Task<ICollection<T>> GetAllAsyn();
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(int id);
        Task<T> GetAsync(long id);

        int Count();
        Task<int> CountAsync();

        T Add(T t);
        Task<T> AddAsyn(T t);
        void Save();
        Task<int> SaveAsync();
        T Update(T t, object key);
        Task<T> UpdateAsyn(T t, object key);
        void Delete(T entity);
        Task<int> DeleteAsyn(T entity);

        void Dispose();

    }
}
