using Insurance.Core.Domain.Entities;

namespace Insurance.Test.Mocks.Entities
{
    public class CarrierMock : MockBuilder<CarrierMock, Carrier>
    {
        public static Carrier Get(string key)
        {
            return Create(key).Default().Build();
        }

        public CarrierMock Default()
        {
            Value.Name = Fake.GetName(Key);
            Value.Address = Fake.GetAddress(Key);
            Value.Phone = Fake.GetPhone();

            return this;
        }
    }
}