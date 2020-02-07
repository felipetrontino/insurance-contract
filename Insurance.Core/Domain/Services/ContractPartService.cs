using Insurance.Core.Domain.Common;
using Insurance.Core.Domain.Entities;
using Insurance.Core.Domain.Exceptions;
using Insurance.Core.Domain.Interfaces.Entity;
using Insurance.Core.Domain.Interfaces.Model;
using Insurance.Core.Domain.Interfaces.Service;
using Insurance.Core.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Core.Domain.Services
{
    public abstract class ContractPartService<TInputModel, TViewModel, TEntity> : IContractPartService<TInputModel, TViewModel>
        where TInputModel : class, IContractPart
        where TViewModel : class, IContractPart
        where TEntity : class, IContractPartEntity
    {
        private readonly InsuranceDb _db;

        protected ContractPartService(InsuranceDb db)
        {
            _db = db;
        }

        public async Task Add(TInputModel model)
        {
            if (model == null)
                throw new ValidationBusinessException(ValidationMessage.InputInvalid);

            if (string.IsNullOrEmpty(model.Name))
                throw new ValidationBusinessException(ValidationMessage.NameInvalid);

            var entity = MapFromModel(model, null);

            await _db.Set<TEntity>().AddAsync(entity);

            await _db.SaveChangesAsync();
        }

        public async Task Update(Guid id, TInputModel model)
        {
            if (model == null)
                throw new ValidationBusinessException(ValidationMessage.InputInvalid);

            if (string.IsNullOrEmpty(model.Name))
                throw new ValidationBusinessException(ValidationMessage.NameInvalid);

            var entity = await _db.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new ValidationBusinessException(ValidationMessage.EntityNotFound);

            entity = MapFromModel(model, entity);

            _db.Set<TEntity>().Update(entity);

            await _db.SaveChangesAsync();
        }

        public async Task<TViewModel> Get(Guid id)
        {
            if (id == Guid.Empty)
                throw new ValidationBusinessException(ValidationMessage.IdInvalid);

            var entity = await _db.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new ValidationBusinessException(ValidationMessage.EntityNotFound);

            return MapToModel(entity);
        }

        public async Task<List<TViewModel>> GetAll()
        {
            var entities = await _db.Set<TEntity>().AsNoTracking().ToListAsync();
            return entities.Select(MapToModel).ToList();
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

            _db.Set<Contract>().RemoveRange(contracts);

            _db.Set<TEntity>().Remove(entity);

            await _db.SaveChangesAsync();
        }

        protected abstract TEntity MapFromModel(TInputModel model, TEntity entity);

        protected abstract TViewModel MapToModel(TEntity entity);
    }
}