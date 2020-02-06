using Insurance.Core.Domain.Entities;
using Insurance.Core.Domain.Interfaces.Service;
using Insurance.Core.Infra.Data;

namespace Insurance.Core.Domain.Services
{
    public class CarrierService : ContractPartService<Carrier>, ICarrierService
    {
        public CarrierService(InsuranceDb db) : base(db)
        {
        }
    }
}