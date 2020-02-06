using System;

namespace Insurance.Core.Domain.Interfaces.Entity
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}