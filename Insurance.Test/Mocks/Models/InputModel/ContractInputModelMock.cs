using Insurance.Core.Domain.Models.InputModel;
using System;

namespace Insurance.Test.Mocks.Models.InputModel
{
    public class ContractInputModelMock : MockBuilder<ContractInputModelMock, ContractInputModel>
    {
        public static ContractInputModel Get(Guid fromId, Guid toId)
        {
            return Create(Fake.GetKey()).Default(fromId, toId).Build();
        }

        public ContractInputModelMock Default(Guid fromId, Guid toId)
        {
            Value.FromId = fromId;
            Value.ToId = toId;

            return this;
        }
    }
}