using Insurance.Core.Domain.Entities;
using Insurance.Core.Domain.Interfaces.Service;
using Insurance.Core.Infra.Data;

namespace Insurance.Core.Domain.Services
{
    public class AdvisorService : ContractPartService<Advisor>, IAdvisorService
    {
        public AdvisorService(InsuranceDb db) : base(db)
        {
        }
    }
}