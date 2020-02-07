using System;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Core.Interfaces
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class, IEntity
    {
        Task Add(TEntity entity);

        void Update(TEntity entity);

        IQueryable<TEntity> Query();

        Task<TEntity> Get(Guid id);
    }
}