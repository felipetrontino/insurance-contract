using System;

namespace Insurance.Core.Domain.Models.InputModel
{
    public class ContractInputModel
    {
        public Guid FromId { get; set; }

        public Guid ToId { get; set; }
    }
}