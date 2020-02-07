using Insurance.Domain.Enums;
using Insurance.Core.Interfaces;

namespace Insurance.Domain.Entities
{
    public class ContractPart : BaseEntity, IContractPartEntity
    {
        public ContractPartType Type { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
    }
}