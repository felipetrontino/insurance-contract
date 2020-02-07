using Insurance.Domain.Entities;
using Insurance.Domain.Interfaces.Repository;
using Insurance.Infra.Data;
using System;
using System.Linq;

namespace Insurance.Infra.Repositories
{
    public class ContractRepository : Repository<Contract>, IContractRepository
    {
        public ContractRepository(InsuranceDb db) : base(db)
        {
        }

        public IQueryable<Contract> QueryMatchContract(Guid partOneId, Guid partTwoId)
        {
            return DbSet.Where(x => (x.FromId == partOneId && x.ToId == partTwoId)
                                  || (x.FromId == partTwoId && x.ToId == partOneId));
        }
    }
}