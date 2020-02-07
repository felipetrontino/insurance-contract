using Insurance.Domain.Entities;
using Insurance.Domain.Interfaces.Repository;
using Insurance.Infra.Data;

namespace Insurance.Infra.Repositories
{
    public class CarrierRepository : ContractPartRepository<Carrier>, ICarrierRepository
    {
        public CarrierRepository(InsuranceDb db) : base(db)
        {
        }
    }
}