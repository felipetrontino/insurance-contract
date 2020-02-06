using Insurance.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Core.Domain.Interfaces.Service
{
    public interface IContractPartService<TEntity>
        where TEntity : ContractPart
    {
        Task Add(TEntity model);

        Task Update(TEntity model);

        Task<TEntity> Get(Guid id);

        Task<List<TEntity>> GetAll();

        Task Delete(Guid id);
    }
}