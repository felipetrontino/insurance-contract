using Insurance.Domain.Entities;
using Insurance.Application.Interfaces;
using Insurance.Application.Models.InputModel;
using Insurance.Application.Models.ViewModel;
using Insurance.Infra.Data;

namespace Insurance.Domain.Services
{
    public class MgaAppService : ContractPartAppService<MgaInputModel, MgaViewModel, Mga>, IMgaAppService
    {
        public MgaAppService(InsuranceDb db) : base(db)
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