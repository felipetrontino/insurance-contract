using Insurance.Application.Models.ViewModel;

namespace Insurance.Test.Mocks.Models.ViewModel
{
    public class ContractPartViewModelMock : MockBuilder<ContractPartViewModelMock, ContractPartViewModel>
    {
        public static ContractPartViewModel Get(string key)
        {
            return Create(key).Default().Build();
        }

        public ContractPartViewModelMock Default()
        {
            Value.Name = Fake.GetName(Key);
            Value.Address = Fake.GetAddress(Key);
            Value.Phone = Fake.GetPhone();

            return this;
        }
    }
}