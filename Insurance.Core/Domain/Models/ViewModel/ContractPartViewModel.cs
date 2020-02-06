using System;

namespace Insurance.Core.Domain.Models.ViewModel
{
    public class ContractPartViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}