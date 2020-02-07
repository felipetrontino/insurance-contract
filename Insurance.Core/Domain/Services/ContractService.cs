using Insurance.Core.Domain.Common;
using Insurance.Core.Domain.Core;
using Insurance.Core.Domain.Entities;
using Insurance.Core.Domain.Exceptions;
using Insurance.Core.Domain.Interfaces.Service;
using Insurance.Core.Domain.Models.InputModel;
using Insurance.Core.Domain.Models.ViewModel;
using Insurance.Core.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Core.Domain.Services
{
    public class ContractService : IContractService
    {
        private readonly InsuranceDb _db;
        private readonly IPathFinder _pathFinder;

        public ContractService(InsuranceDb db, IPathFinder pathFinder)
        {
            _db = db;
            _pathFinder = pathFinder;
        }

        public async Task Establish(ContractInputModel model)
        {
            if (model == null)
                throw new ValidationBusinessException(ValidationMessage.InputInvalid);

            if (model.FromId == Guid.Empty || model.ToId == Guid.Empty)
                throw new ValidationBusinessException(ValidationMessage.IdInvalid);

            if (model.FromId == model.ToId)
                throw new ValidationBusinessException(ValidationMessage.ContractInvalid);

            var exists = Match(model).Any();

            if (exists)
                throw new ValidationBusinessException(ValidationMessage.ContractExists);

            var contract = new Contract() { FromId = model.FromId, ToId = model.ToId };

            await _db.AddAsync(contract);
            await _db.SaveChangesAsync();
        }

        public async Task Terminate(ContractInputModel model)
        {
            if (model == null)
                throw new ValidationBusinessException(ValidationMessage.InputInvalid);

            if (model.FromId == Guid.Empty || model.ToId == Guid.Empty)
                throw new ValidationBusinessException(ValidationMessage.IdInvalid);

            if (model.FromId == model.ToId)
                throw new ValidationBusinessException(ValidationMessage.ContractInvalid);

            var exists = Match(model).FirstOrDefault();

            if (exists == null)
                throw new ValidationBusinessException(ValidationMessage.ContractNotExists);

            exists.Finished = true;

            await _db.SaveChangesAsync();
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

            var ids = _pathFinder.FindShortestPath(nodes.Select(x => x.Id).ToArray(), edges.Select(x => new Guid[] { x.FromId, x.ToId }).ToList(), model.FromId, model.ToId);

            var parts = await _db.Set<ContractPart>().Where(x => ids.Contains(x.Id)).ToListAsync();

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
            return (from contract in _db.Set<Contract>()
                    join fromPart in _db.Set<ContractPart>() on contract.FromId equals fromPart.Id
                    join toPart in _db.Set<ContractPart>() on contract.ToId equals toPart.Id
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
            return await _db.Set<ContractPart>()
                .AsNoTracking()
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
            return await (from part in _db.Set<ContractPart>()
                          select new NodeViewModel()
                          {
                              Id = part.Id,
                              Name = part.Name
                          })
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<List<EdgeViewModel>> GetEdges()
        {
            return await (from contract in _db.Set<Contract>()
                          where !contract.Finished
                          select new EdgeViewModel()
                          {
                              FromId = contract.FromId,
                              ToId = contract.ToId
                          })
                          .AsNoTracking()
                          .ToListAsync();
        }

        private IQueryable<Contract> Match(ContractInputModel model)
        {
            return _db.Set<Contract>()
                              .Where(x => (x.FromId == model.FromId && x.ToId == model.ToId)
                                  || (x.FromId == model.ToId && x.ToId == model.FromId));
        }
    }
}