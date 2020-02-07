using Insurance.Core.Domain.Models.InputModel;

namespace Insurance.Test.Mocks.Models.InputModel
{
    public class MgaInputModelMock : MockBuilder<MgaInputModelMock, MgaInputModel>
    {
        public static MgaInputModel Get(string key)
        {
            return Create(key).Default().Build();
        }

        public MgaInputModelMock Default()
        {
            Value.Name = Fake.GetName(Key);
            Value.Address = Fake.GetAddress(Key);
            Value.Phone = Fake.GetPhone();

            return this;
        }
    }
}