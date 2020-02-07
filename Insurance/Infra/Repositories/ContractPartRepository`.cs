using Insurance.Core.Interfaces;
using Insurance.Domain.Entities;
using Insurance.Domain.Interfaces.Repository;
using Insurance.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Infra.Repositories
{
    public class ContractPartRepository<TEntity> : Repository<TEntity>, IContractPartRepository<TEntity>
        where TEntity : class, IContractPartEntity
    {
        public ContractPartRepository(InsuranceDb db) : base(db)
        {
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public void Delete(TEntity entity)
        {
            var contractDbSet = Db.Set<Contract>();

            var contracts = contractDbSet
                                .Where(x => x.FromId == entity.Id || x.ToId == entity.Id)
                                .ToList();

            contractDbSet.RemoveRange(contracts);

            DbSet.Remove(entity);
        }
    }
}