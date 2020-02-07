using Insurance.Domain.Entities;
using Insurance.Application.Models.ViewModel;

namespace Insurance.Test.Mocks.Models.ViewModel
{
    public class ContractViewModelMock : MockBuilder<ContractViewModelMock, ContractViewModel>
    {
        public static ContractViewModel Get(ContractPart from, ContractPart to)
        {
            return Create(Fake.GetKey()).Default(from, to).Build();
        }

        public ContractViewModelMock Default(ContractPart from, ContractPart to)
        {
            Value.From = GetPart(from);
            Value.To = GetPart(to);
            Value.Finished = false;

            return this;
        }

        private ContractViewModel.Part GetPart(ContractPart part)
        {
            return new ContractViewModel.Part()
            {
                Id = part.Id,
                Name = part.Name,
                Address = part.Address,
                Phone = part.Phone
            };
        }
    }
}