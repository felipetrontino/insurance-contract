using Insurance.Core.Domain.Entities;
using Insurance.Core.Domain.Interfaces.Service;
using Insurance.Core.Domain.Models.InputModel;
using Insurance.Core.Domain.Models.ViewModel;
using Insurance.Core.Infra.Data;

namespace Insurance.Core.Domain.Services
{
    public class MgaService : ContractPartService<MgaInputModel, MgaViewModel, Mga>, IMgaService
    {
        public MgaService(InsuranceDb db) : base(db)
        {
        }

        protected override Mga MapFromModel(MgaInputModel model, Mga entity)
        {
            entity ??= new Mga();
            entity.Name = model.Name;
            entity.Address = model.Address;
            entity.Phone = model.Phone;

            return entity;
        }

        protected override MgaViewModel MapToModel(Mga entity)
        {
            return new MgaViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Address = entity.Address,
                Phone = entity.Phone
            };
        }
    }
}