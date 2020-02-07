using System;

namespace Insurance.Domain.Entities
{
    public class Contract : BaseEntity
    {
        public Guid FromId { get; set; }

        public Guid ToId { get; set; }

        public bool Finished { get; set; }
    }
}