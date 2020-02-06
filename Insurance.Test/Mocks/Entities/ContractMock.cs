using Insurance.Core.Domain.Entities;
using System;

namespace Insurance.Test.Mocks.Entities
{
    public class ContractMock : MockBuilder<ContractMock, Contract>
    {
        public static Contract Get(Guid fromId, Guid toId)
        {
            return Create(Fake.GetKey()).Default(fromId, toId).Build();
        }

        public ContractMock Default(Guid fromId, Guid toId)
        {
            Value.FromId = fromId;
            Value.ToId = toId;
            Value.Finished = false;

            return this;
        }
    }
}