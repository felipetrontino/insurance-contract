using Insurance.Core.Domain.Entities;

namespace Insurance.Test.Mocks.Entities
{
    public class AdvisorMock : MockBuilder<AdvisorMock, Advisor>
    {
        public static Advisor Get(string key)
        {
            return Create(key).Default().Build();
        }

        public AdvisorMock Default()
        {
            Value.Name = Fake.GetName(Key);
            Value.Address = Fake.GetAddress(Key);
            Value.Phone = Fake.GetPhone();
            Value.LastName = Fake.GetLastName(Key);
            Value.HealthStatus = HealthStatus.Green;

            return this;
        }
    }
}