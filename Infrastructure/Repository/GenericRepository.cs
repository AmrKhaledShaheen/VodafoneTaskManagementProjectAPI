using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> _dbSet;
        private DbContextTask _dbContext;
        public GenericRepository(DbContextTask dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<TEntity> Get(int pageSize, int currentPage, out int count)
        {
            IEnumerable<TEntity> query = _dbSet;
            count = query.Count();
            return query.Skip((pageSize * currentPage) - pageSize).Take(pageSize).ToList();
        }

        IQueryable<TEntity> IGenericRepository<TEntity>.GetFiltered(Expression<Func<TEntity, bool>> where, string includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;
            if (includeProperties != "")
            {
                foreach (var includeProperty in includeProperties.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.Where(where);
        }

        public IQueryable<TResult> GetFiltered<TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TResult>> selector, string includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.Where(where).Select(selector);
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
            _dbContext.Entry(entity).State = EntityState.Added;
        }

        public TEntity Create(TEntity entity)
        {
            _dbSet.Add(entity);
            _dbContext.Entry(entity).State = EntityState.Added;
            return entity;
        }
        public TEntity CreateAsNoTracking(TEntity entity)
        {
            _dbSet.Add(entity);
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {

            foreach (var entity in entities)
            {
                _dbSet.Add(entity);
                _dbContext.Entry(entity).State = EntityState.Added;
            }
        }
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
          
            foreach (var entity in entities)
            {
                _dbSet.Attach(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public TEntity Get(int Id)
        {
            return _dbSet.Find(Id);
        }

        public TEntity Get(string Id)
        {
            return _dbSet.Find(Id);
        }
        public void Delete(int Id)
        {
            var entity = _dbSet.Find(Id);
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }
        #region Asynchronous
        public async Task<IQueryable<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> where, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await Task.FromResult(query.Where(where));
        }
        public async Task<IQueryable<TResult>> GetFilteredAsync<TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TResult>> selector, string includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;

            // Apply includes
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            // Apply filtering and selection
            return query.Where(where).Select(selector);
        }

        #endregion

    }
}
