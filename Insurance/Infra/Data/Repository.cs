using Insurance.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Infra.Data
{
    [ExcludeFromCodeCoverage]
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly InsuranceDb Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(InsuranceDb db)
        {
            Db = db;
            DbSet = Db.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public async Task<TEntity> Get(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public IQueryable<TEntity> Query()
        {
            return DbSet;
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Db.Dispose();
        }

        ~Repository()
        {
            Dispose(false);
        }

        #endregion IDisposable
    }
}