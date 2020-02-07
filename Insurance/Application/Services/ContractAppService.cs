using Insurance.Application.Interfaces;
using Insurance.Application.Models.InputModel;
using Insurance.Application.Models.ViewModel;
using Insurance.Core.Exceptions;
using Insurance.Core.Interfaces;
using Insurance.Domain.Common;
using Insurance.Domain.Entities;
using Insurance.Domain.Interfaces.Repository;
using Insurance.Domain.Interfaces.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Domain.Services
{
    public class ContractAppService : IContractAppService
    {
        private readonly IContractRepository _repo;
        private readonly IPathFinderService _pathFinderService;
        private readonly IUnitOfWork _uow;
        private readonly IContractPartRepository _contractPartRepo;

        public ContractAppService(IContractRepository repo, IContractPartRepository contractPartRepo, IUnitOfWork uow, IPathFinderService pathFinderService)
        {
            _repo = repo;
            _contractPartRepo = contractPartRepo;
            _uow = uow;
            _pathFinderService = pathFinderService;
        }

        public async Task Establish(ContractInputModel model)
        {
            if (model == null)
                throw new ValidationBusinessException(ValidationMessage.InputInvalid);

            if (model.FromId == Guid.Empty || model.ToId == Guid.Empty)
                throw new ValidationBusinessException(ValidationMessage.IdInvalid);

            if (model.FromId == model.ToId)
                throw new ValidationBusinessException(ValidationMessage.ContractInvalid);

            var exists = _repo.QueryMatchContract(model.FromId, model.ToId).Any(x => !x.Finished);

            if (exists)
                throw new ValidationBusinessException(ValidationMessage.ContractExists);

            var contract = new Contract() { FromId = model.FromId, ToId = model.ToId };

            await _repo.Add(contract);

            await _uow.Commit();
        }

        public async Task Terminate(ContractInputModel model)
        {
            if (model == null)
                throw new ValidationBusinessException(ValidationMessage.InputInvalid);

            if (model.FromId == Guid.Empty || model.ToId == Guid.Empty)
                throw new ValidationBusinessException(ValidationMessage.IdInvalid);

            if (model.FromId == model.ToId)
                throw new ValidationBusinessException(ValidationMessage.ContractInvalid);

            var entity = _repo.QueryMatchContract(model.FromId, model.ToId).FirstOrDefault();

            if (entity == null)
                throw new ValidationBusinessException(ValidationMessage.ContractNotExists);

            if (entity.Finished)
                throw new ValidationBusinessException(ValidationMessage.ContractFinished);

            entity.Finished = true;

            _repo.Update(entity);

            await _uow.Commit();
        }

        public async Task<List<ContractPartViewModel>> FindShortestPath(ContractInputModel model)
        {
            if (model == null)
                throw new ValidationBusinessException(ValidationMessage.InputInvalid);

            if (model.FromId == Guid.Empty || model.ToId == Guid.Empty)
                throw new ValidationBusinessException(ValidationMessage.IdInvalid);

            if (model.FromId == model.ToId)
                throw new ValidationBusinessException(ValidationMessage.ContractInvalid);

            var result = Enumerable.Empty<ContractPartViewModel>().ToList();

            var nodes = await GetNodes();

            if (nodes.Count == 0) return result;

            var edges = await GetEdges();

            if (edges.Count == 0) return result;

            var ids = _pathFinderService.FindShortestPath(nodes.Select(x => x.Id).ToArray(), edges.Select(x => new Guid[] { x.FromId, x.ToId }).ToList(), model.FromId, model.ToId);

            var parts = await _contractPartRepo.GetAll(ids);

            result = parts.Select(x => new ContractPartViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                Phone = x.Phone
            })
            .OrderBy(x => Array.IndexOf(ids, x.Id))
            .ToList();

            return result;
        }

        public Task<List<ContractViewModel>> GetAll()
        {
            return (from contract in _repo.Query()
                    join fromPart in _contractPartRepo.Query() on contract.FromId equals fromPart.Id
                    join toPart in _contractPartRepo.Query() on contract.ToId equals toPart.Id
                    select new ContractViewModel()
                    {
                        From = new ContractViewModel.Part()
                        {
                            Id = fromPart.Id,
                            Name = fromPart.Name,
                            Address = fromPart.Address,
                            Phone = fromPart.Phone
                        },
                        To = new ContractViewModel.Part()
                        {
                            Id = toPart.Id,
                            Name = toPart.Name,
                            Address = toPart.Address,
                            Phone = toPart.Phone
                        },
                        Finished = contract.Finished
                    })
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<List<ContractPartViewModel>> GetParts()
        {
            return await _contractPartRepo.Query()
                                         .Select(x => new ContractPartViewModel()
                                         {
                                             Id = x.Id,
                                             Name = x.Name,
                                             Address = x.Address,
                                             Phone = x.Phone
                                         })
                                         .ToListAsync();
        }

        public async Task<List<NodeViewModel>> GetNodes()
        {
            return await _contractPartRepo.Query()
                          .Select(x => new NodeViewModel()
                          {
                              Id = x.Id,
                              Name = x.Name
                          })
                        .AsNoTracking()
                        .ToListAsync();
        }

        public async Task<List<EdgeViewModel>> GetEdges()
        {
            return await (from contract in _repo.Query()
                          where !contract.Finished
                          select new EdgeViewModel()
                          {
                              FromId = contract.FromId,
                              ToId = contract.ToId
                          })
                          .AsNoTracking()
                          .ToListAsync();
        }
    }
}