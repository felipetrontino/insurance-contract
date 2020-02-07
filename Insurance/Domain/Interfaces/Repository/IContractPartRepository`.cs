using Insurance.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Domain.Interfaces.Repository
{
    public interface IContractPartRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IContractPartEntity
    {
        Task<IEnumerable<TEntity>> GetAll();

        void Delete(TEntity entity);
    }
}