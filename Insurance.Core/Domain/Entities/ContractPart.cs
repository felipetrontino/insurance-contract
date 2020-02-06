using Insurance.Core.Domain.Enums;

namespace Insurance.Core.Domain.Entities
{
    public class ContractPart : BaseEntity
    {
        public ContractPartType Type { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
    }
}