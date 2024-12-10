using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Get(int pageSize, int currentPage, out int count);
        IQueryable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> where, string includeProperties = "");
        IQueryable<TResult> GetFiltered<TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TResult>> selector, string includeProperties);
        void Add(TEntity entity);
        TEntity Create(TEntity entity);
        TEntity CreateAsNoTracking(TEntity entity);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void AddRange(IEnumerable<TEntity> entities);
        TEntity Get(int Id);
        TEntity Get(string Id);
        void Delete(int Id);
        void DeleteRange(IEnumerable<TEntity> entities);
        #region Asynchronous
        Task<IQueryable<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> where, string includeProperties = "");
        Task<IQueryable<TResult>> GetFilteredAsync<TResult>(Expression<Func<TEntity, bool>> where,Expression<Func<TEntity, TResult>> selector, string includeProperties);
        #endregion

    }
}
