using Insurance.Core.Domain.Interfaces.Entity;
using System;

namespace Insurance.Core.Domain.Entities
{
    public abstract class BaseEntity : IEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}