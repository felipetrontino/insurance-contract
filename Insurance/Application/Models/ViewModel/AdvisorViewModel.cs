using Insurance.Domain.Entities;
using Insurance.Domain.Interfaces.Model;
using System;

namespace Insurance.Application.Models.ViewModel
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