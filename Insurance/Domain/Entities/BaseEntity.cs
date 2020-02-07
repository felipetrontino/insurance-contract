using Insurance.Core.Interfaces;
using System;

namespace Insurance.Domain.Entities
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