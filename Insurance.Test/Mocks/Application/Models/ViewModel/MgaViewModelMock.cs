using Insurance.Application.Models.ViewModel;

namespace Insurance.Test.Mocks.Models.InputModel
{
    public class MgaViewModelMock : MockBuilder<MgaViewModelMock, MgaViewModel>
    {
        public static MgaViewModel Get(string key)
        {
            return Create(key).Default().Build();
        }

        public MgaViewModelMock Default()
        {
            Value.Name = Fake.GetName(Key);
            Value.Address = Fake.GetAddress(Key);
            Value.Phone = Fake.GetPhone();

            return this;
        }
    }
}