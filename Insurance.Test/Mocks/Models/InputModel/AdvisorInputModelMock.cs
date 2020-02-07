using Insurance.Core.Domain.Models.InputModel;

namespace Insurance.Test.Mocks.Models.InputModel
{
    public class AdvisorInputModelMock : MockBuilder<AdvisorInputModelMock, AdvisorInputModel>
    {
        public static AdvisorInputModel Get(string key)
        {
            return Create(key).Default().Build();
        }

        public AdvisorInputModelMock Default()
        {
            Value.Name = Fake.GetName(Key);
            Value.LastName = Fake.GetLastName(Key);
            Value.Address = Fake.GetAddress(Key);
            Value.Phone = Fake.GetPhone();

            return this;
        }
    }
}