using Insurance.Core.Domain.Models.ViewModel;
using System;

namespace Insurance.Test.Mocks.Models.ViewModel
{
    public class EdgeViewModelMock : MockBuilder<EdgeViewModelMock, EdgeViewModel>
    {
        public static EdgeViewModel Get(Guid fromId, Guid toId)
        {
            return Create(Fake.GetKey()).Default(fromId, toId).Build();
        }

        public EdgeViewModelMock Default(Guid fromId, Guid toId)
        {
            Value.FromId = fromId;
            Value.ToId = toId;

            return this;
        }
    }
}