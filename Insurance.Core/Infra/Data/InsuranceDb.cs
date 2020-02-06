using Insurance.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Insurance.Core.Infra.Data
{
    [ExcludeFromCodeCoverage]
    public class InsuranceDb : DbContext
    {
        public InsuranceDb(DbContextOptions options)
            : base(options)
        { }

        public DbSet<ContractPart> ContractParts { get; set; }

        public DbSet<Advisor> Advisors { get; set; }

        public DbSet<Carrier> Carriers { get; set; }

        public DbSet<Mga> Mgas { get; set; }

        public DbSet<Contract> Contracts { get; set; }
    }
}