using Insurance.Core.Interfaces;
using Insurance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Domain.Interfaces.Repository
{
    public interface IContractPartRepository : IRepository<ContractPart>
    {
        Task<IEnumerable<ContractPart>> GetAll(params Guid[] ids);
    }
}