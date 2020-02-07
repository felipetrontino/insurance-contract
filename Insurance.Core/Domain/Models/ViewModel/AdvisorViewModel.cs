using Insurance.Core.Domain.Entities;
using Insurance.Core.Domain.Interfaces.Model;
using System;

namespace Insurance.Core.Domain.Models.ViewModel
{
    public class AdvisorViewModel : IContractPart
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public HealthStatus HealthStatus { get; set; }
    }
}