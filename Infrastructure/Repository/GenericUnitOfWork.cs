
using Infrastructure.DataContext;

namespace Infrastructure.Repository
{
    public class GenericUnitOfWork : IUnitOfWork
    {
        private DbContextTask _dbContext;

        public GenericUnitOfWork(DbContextTask dbcontext)
        {
            _dbContext = dbcontext;
        }

        public Type TheType { get; set; }

        public IGenericRepository<TEntityType> GetRepoInstance<TEntityType>() where TEntityType : class
        {
            return new GenericRepository<TEntityType>(_dbContext);
        }

        public bool SaveChanges()
        {
            try
            {
                if (_dbContext.SaveChangesAsync().Result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
