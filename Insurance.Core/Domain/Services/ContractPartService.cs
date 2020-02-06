using Insurance.Core.Domain.Common;
using Insurance.Core.Domain.Entities;
using Insurance.Core.Domain.Exceptions;
using Insurance.Core.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Core.Domain.Services
{
    public class ContractPartService<TEntity>
        where TEntity : ContractPart
    {
        private readonly InsuranceDb _db;

        public ContractPartService(InsuranceDb db)
        {
            _db = db;
        }

        public async Task Add(TEntity entity)
        {
            if (entity == null)
                throw new ValidationBusinessException(ValidationMessage.InputInvalid);

            if (string.IsNullOrEmpty(entity.Name))
                throw new ValidationBusinessException(ValidationMessage.NameInvalid);

            await _db.Set<TEntity>().AddAsync(entity);

            await _db.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            if (entity == null)
                throw new ValidationBusinessException(ValidationMessage.InputInvalid);

            if (string.IsNullOrEmpty(entity.Name))
                throw new ValidationBusinessException(ValidationMessage.NameInvalid);

            _db.Set<TEntity>().Update(entity);

            await _db.SaveChangesAsync();
        }

        public async Task<TEntity> Get(Guid id)
        {
            if (id == Guid.Empty)
                throw new ValidationBusinessException(ValidationMessage.IdInvalid);

            return await _db.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _db.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ValidationBusinessException(ValidationMessage.IdInvalid);

            var entity = await _db.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new ValidationBusinessException(ValidationMessage.EntityNotFound);

            var contracts = _db.Set<Contract>()
                                .Where(x => x.FromId == id || x.ToId == id)
                                .ToList();
            _db.RemoveRange(contracts);

            _db.Set<TEntity>().Remove(entity);

            await _db.SaveChangesAsync();
        }
    }
}