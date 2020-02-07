using Insurance.Core.Domain.Entities;
using Insurance.Core.Domain.Interfaces.Service;
using Insurance.Core.Domain.Models.InputModel;
using Insurance.Core.Domain.Models.ViewModel;
using Insurance.Core.Infra.Data;

namespace Insurance.Core.Domain.Services
{
    public class CarrierService : ContractPartService<CarrierInputModel, CarrierViewModel, Carrier>, ICarrierService
    {
        public CarrierService(InsuranceDb db) : base(db)
        {
        }

        protected override Carrier MapFromModel(CarrierInputModel model, Carrier entity)
        {
            entity ??= new Carrier();
            entity.Name = model.Name;
            entity.Address = model.Address;
            entity.Phone = model.Phone;

            return entity;
        }

        protected override CarrierViewModel MapToModel(Carrier entity)
        {
            return new CarrierViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Address = entity.Address,
                Phone = entity.Phone
            };
        }
    }
}