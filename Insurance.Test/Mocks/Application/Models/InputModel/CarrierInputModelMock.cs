using Insurance.Application.Models.InputModel;

namespace Insurance.Test.Mocks.Models.InputModel
{
    public class CarrierInputModelMock : MockBuilder<CarrierInputModelMock, CarrierInputModel>
    {
        public static CarrierInputModel Get(string key)
        {
            return Create(key).Default().Build();
        }

        public CarrierInputModelMock Default()
        {
            Value.Name = Fake.GetName(Key);
            Value.Address = Fake.GetAddress(Key);
            Value.Phone = Fake.GetPhone();

            return this;
        }
    }
}