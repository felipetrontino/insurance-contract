using Insurance.Core.Domain.Entities;
using Insurance.Core.Domain.Interfaces.Service;
using Insurance.Core.Infra.Data;

namespace Insurance.Core.Domain.Services
{
    public class MgaService : ContractPartService<Mga>, IMgaService
    {
        public MgaService(InsuranceDb db) : base(db)
        {
        }
    }
}