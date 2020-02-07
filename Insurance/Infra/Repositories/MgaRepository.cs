using Insurance.Domain.Entities;
using Insurance.Domain.Interfaces.Repository;
using Insurance.Infra.Data;

namespace Insurance.Infra.Repositories
{
    public class MgaRepository : ContractPartRepository<Mga>, IMgaRepository
    {
        public MgaRepository(InsuranceDb db) : base(db)
        {
        }
    }
}