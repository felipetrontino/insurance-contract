using Insurance.Core.Interfaces;
using Insurance.Domain.Entities;
using System;
using System.Linq;

namespace Insurance.Domain.Interfaces.Repository
{
    public interface IContractRepository : IRepository<Contract>
    {
        IQueryable<Contract> QueryMatchContract(Guid partOneId, Guid partTwoId);
    }
}