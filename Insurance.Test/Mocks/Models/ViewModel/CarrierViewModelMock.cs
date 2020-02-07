using Insurance.Core.Domain.Models.ViewModel;

namespace Insurance.Test.Mocks.Models.InputModel
{
    public class CarrierViewModelMock : MockBuilder<CarrierViewModelMock, CarrierViewModel>
    {
        public static CarrierViewModel Get(string key)
        {
            return Create(key).Default().Build();
        }

        public CarrierViewModelMock Default()
        {
            Value.Name = Fake.GetName(Key);
            Value.Address = Fake.GetAddress(Key);
            Value.Phone = Fake.GetPhone();

            return this;
        }
    }
}