using Insurance.Domain.Entities;
using Insurance.Application.Models.ViewModel;

namespace Insurance.Test.Mocks.Models.InputModel
{
    public class AdvisorViewModelMock : MockBuilder<AdvisorViewModelMock, AdvisorViewModel>
    {
        public static AdvisorViewModel Get(string key)
        {
            return Create(key).Default().Build();
        }

        public AdvisorViewModelMock Default()
        {
            Value.Name = Fake.GetName(Key);
            Value.LastName = Fake.GetLastName(Key);
            Value.Address = Fake.GetAddress(Key);
            Value.Phone = Fake.GetPhone();
            Value.HealthStatus = HealthStatus.Green;

            return this;
        }
    }
}