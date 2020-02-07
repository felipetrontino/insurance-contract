using Insurance.Core.Domain.Interfaces.Model;
using System;

namespace Insurance.Core.Domain.Models.ViewModel
{
    public class MgaViewModel : IContractPart
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
    }
}