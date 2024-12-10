using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        Type TheType { get; set; }

        IGenericRepository<TEntityType> GetRepoInstance<TEntityType>() where TEntityType : class;

        bool SaveChanges();
    }

}
