using System;

namespace Insurance.Application.Models.InputModel
{
    public class ContractInputModel
    {
        public Guid FromId { get; set; }

        public Guid ToId { get; set; }
    }
}