using Insurance.Core.Domain.Entities;

namespace Insurance.Test.Mocks.Entities
{
    public class MgaMock : MockBuilder<MgaMock, Mga>
    {
        public static Mga Get(string key)
        {
            return Create(key).Default().Build();
        }

        public MgaMock Default()
        {
            Value.Name = Fake.GetName(Key);
            Value.Address = Fake.GetAddress(Key);
            Value.Phone = Fake.GetPhone();

            return this;
        }
    }
}