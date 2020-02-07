using Insurance.Domain.Entities;
using Insurance.Domain.Interfaces.Repository;
using Insurance.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Infra.Repositories
{
    public class ContractPartRepository : Repository<ContractPart>, IContractPartRepository
    {
        public ContractPartRepository(InsuranceDb db) : base(db)
        {
        }

        public async Task<IEnumerable<ContractPart>> GetAll(params Guid[] ids)
        {
            return await DbSet.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}