using Insurance.Domain.Entities;
using Insurance.Domain.Enums;

namespace Insurance.Test.Mocks.Domain.Entities
{
    public class CarrierMock : MockBuilder<CarrierMock, Carrier>
    {
        public static Carrier Get(string key)
        {
            return Create(key).Default().Build();
        }

        public CarrierMock Default()
        {
            Value.Type = ContractPartType.Carrier;
            Value.Name = Fake.GetName(Key);
            Value.Address = Fake.GetAddress(Key);
            Value.Phone = Fake.GetPhone();

            return this;
        }
    }
}