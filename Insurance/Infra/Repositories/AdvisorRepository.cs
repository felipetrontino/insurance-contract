using Insurance.Domain.Entities;
using Insurance.Domain.Interfaces.Repository;
using Insurance.Infra.Data;

namespace Insurance.Infra.Repositories
{
    public class AdvisorRepository : ContractPartRepository<Advisor>, IAdvisorRepository
    {
        public AdvisorRepository(InsuranceDb db) : base(db)
        {
        }
    }
}