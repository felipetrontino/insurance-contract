using Insurance.Core.Domain.Models.ViewModel;

namespace Insurance.Test.Mocks.Models.ViewModel
{
    public class NodeViewModelMock : MockBuilder<NodeViewModelMock, NodeViewModel>
    {
        public static NodeViewModel Get(string key)
        {
            return Create(key).Default().Build();
        }

        public NodeViewModelMock Default()
        {
            Value.Name = Fake.GetName(Key);

            return this;
        }
    }
}