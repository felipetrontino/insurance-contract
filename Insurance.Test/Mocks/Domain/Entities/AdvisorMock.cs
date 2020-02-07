using Insurance.Domain.Entities;
using Insurance.Domain.Enums;

namespace Insurance.Test.Mocks.Domain.Entities
{
    public class AdvisorMock : MockBuilder<AdvisorMock, Advisor>
    {
        public static Advisor Get(string key)
        {
            return Create(key).Default().Build();
        }

        public AdvisorMock Default()
        {
            Value.Type = ContractPartType.Advisor;
            Value.Name = Fake.GetName(Key);
            Value.LastName = Fake.GetLastName(Key);
            Value.Address = Fake.GetAddress(Key);
            Value.Phone = Fake.GetPhone();
            Value.HealthStatus = HealthStatus.Green;

            return this;
        }
    }
}