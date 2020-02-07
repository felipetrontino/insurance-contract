using Insurance.Application.Interfaces;
using Insurance.Application.Models.InputModel;
using Insurance.Application.Models.ViewModel;
using Insurance.Core.Interfaces;
using Insurance.Domain.Entities;
using Insurance.Domain.Interfaces.Repository;

namespace Insurance.Domain.Services
{
    public class CarrierAppService : ContractPartAppService<CarrierInputModel, CarrierViewModel, Carrier>, ICarrierAppService
    {
        public CarrierAppService(ICarrierRepository repo, IUnitOfWork uow)
            : base(repo, uow)
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