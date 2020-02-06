using Insurance.Core.Domain.Entities;
using Insurance.Core.Domain.Enums;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContractPart>()
                .HasDiscriminator(c => c.Type)
                 .HasValue<ContractPart>(ContractPartType.Undefined)
                .HasValue<Advisor>(ContractPartType.Advisor)
                .HasValue<Carrier>(ContractPartType.Carrier)
                .HasValue<Mga>(ContractPartType.Mga);
        }

        public DbSet<ContractPart> ContractParts { get; set; }

        public DbSet<Advisor> Advisors { get; set; }

        public DbSet<Carrier> Carriers { get; set; }

        public DbSet<Mga> Mgas { get; set; }

        public DbSet<Contract> Contracts { get; set; }
    }
}