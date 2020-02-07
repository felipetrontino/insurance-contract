using Insurance.Core.Domain.Enums;
using Insurance.Core.Domain.Interfaces.Entity;

namespace Insurance.Core.Domain.Entities
{
    public class ContractPart : BaseEntity, IContractPartEntity
    {
        public ContractPartType Type { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
    }
}