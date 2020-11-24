using Data.Layer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Layer.Repository
{
    public abstract class DataRepository<T> : IDataRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        public DataRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Count()
        {
            return _dbContext.Set<T>().Count();
        }

        public virtual async Task<int> CountAsync()
        {
            return await _dbContext.Set<T>().CountAsync();
        }

        public T Get(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            try
            {
                return await _dbContext.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public virtual async Task<T> GetAsync(long id)
        {
            try
            {
                return await _dbContext.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {

            return await _dbContext.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> queryable = _dbContext.Set<T>().Where(predicate);
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
                queryable = queryable.Include<T, object>(includeProperty);

            return await queryable.FirstOrDefaultAsync();

        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }

        public virtual async Task<ICollection<T>> GetAllAsyn()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<ICollection<T>> GetAllAsyn(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual async Task<ICollection<T>> GetAllAsyn(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> queryable = GetAll().Where(predicate);
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
                queryable = queryable.Include<T, object>(includeProperty);

            return await queryable.ToListAsync();

        }

        public virtual async Task<ICollection<T>> GetAllAsyn(Expression<Func<T, bool>> predicate, List<string> includeProperties)
        {

            IQueryable<T> queryable = GetAll().Where(predicate);
            foreach (var includeProperty in includeProperties)
                queryable = queryable.Include(includeProperty);

            return await queryable.ToListAsync();

        }

        public virtual IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
                queryable = queryable.Include<T, object>(includeProperty);

            return queryable;
        }

        public virtual T Find(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().SingleOrDefault(predicate);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate).ToList();
        }

        public virtual async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _dbContext.Set<T>().SingleOrDefaultAsync(predicate);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(predicate);

            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual async Task<T> GetOrderByDescendingAsync<TOrderKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TOrderKey>> orderBy)
        {

            return await _dbContext.Set<T>().Where(predicate).OrderByDescending(orderBy).FirstOrDefaultAsync();
        }

        public T Add(T t)
        {
            _dbContext.Set<T>().Add(t);
            _dbContext.SaveChanges();
            return t;
        }

        public virtual async Task<T> AddAsyn(T t)
        {
            _dbContext.Set<T>().Add(t);
            await _dbContext.SaveChangesAsync();
            return t;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public virtual async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public virtual T Update(T t, object key)
        {
            try
            {
                if (t == null)
                    return null;
                T exist = _dbContext.Set<T>().Find(key);
                if (exist != null)
                {
                    _dbContext.Entry(exist).CurrentValues.SetValues(t);
                    Save();
                }

                return exist;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public virtual async Task<T> UpdateAsyn(T t, object key)
        {
            if (t == null)
                return null;
            T exist = await _dbContext.Set<T>().FindAsync(key);

            if (exist != null)
            {
                _dbContext.Entry(exist).CurrentValues.SetValues(t);
                await _dbContext.SaveChangesAsync();
            }

            return exist;
        }

        public virtual void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            Save();
        }

        public virtual async Task<int> DeleteAsyn(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
