using Insurance.Application.Interfaces;
using Insurance.Core.Exceptions;
using Insurance.Core.Interfaces;
using Insurance.Domain.Common;
using Insurance.Domain.Interfaces.Model;
using Insurance.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Domain.Services
{
    public abstract class ContractPartAppService<TInputModel, TViewModel, TEntity> : IContractPartAppService<TInputModel, TViewModel>
        where TInputModel : class, IContractPart, IValidate
        where TViewModel : class, IContractPart
        where TEntity : class, IContractPartEntity
    {
        protected readonly IContractPartRepository<TEntity> Repo;
        protected readonly IUnitOfWork Uow;

        protected ContractPartAppService(IContractPartRepository<TEntity> repo, IUnitOfWork uow)
        {
            Repo = repo;
            Uow = uow;
        }

        public async Task Add(TInputModel model)
        {
            if (model == null)
                throw new ValidationBusinessException(ValidationMessage.InputInvalid);

            model.Validate();

            var entity = MapFromModel(model, null);

            await Repo.Add(entity);

            await Uow.Commit();
        }

        public async Task Update(Guid id, TInputModel model)
        {
            if (model == null)
                throw new ValidationBusinessException(ValidationMessage.InputInvalid);

            model.Validate();

            var entity = await Repo.Get(id);

            if (entity == null)
                throw new ValidationBusinessException(ValidationMessage.EntityNotFound);

            entity = MapFromModel(model, entity);

            Repo.Update(entity);

            await Uow.Commit();
        }

        public async Task<TViewModel> Get(Guid id)
        {
            if (id == Guid.Empty)
                throw new ValidationBusinessException(ValidationMessage.IdInvalid);

            var entity = await Repo.Get(id);

            if (entity == null)
                throw new ValidationBusinessException(ValidationMessage.EntityNotFound);

            return MapToModel(entity);
        }

        public async Task<List<TViewModel>> GetAll()
        {
            var entities = await Repo.GetAll();
            return entities.Select(MapToModel).ToList();
        }

        public async Task Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ValidationBusinessException(ValidationMessage.IdInvalid);

            var entity = await Repo.Get(id);

            if (entity == null)
                throw new ValidationBusinessException(ValidationMessage.EntityNotFound);

            Repo.Delete(entity);

            await Uow.Commit();
        }

        protected abstract TEntity MapFromModel(TInputModel model, TEntity entity);

        protected abstract TViewModel MapToModel(TEntity entity);
    }
}