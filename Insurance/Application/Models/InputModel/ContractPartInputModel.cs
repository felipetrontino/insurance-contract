using Insurance.Core.Exceptions;
using Insurance.Core.Interfaces;
using Insurance.Domain.Common;
using Insurance.Domain.Interfaces.Model;

namespace Insurance.Application.Models.InputModel
{
    public class ContractPartInputModel : IContractPart, IValidate
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public virtual void Validate()
        {
            if (string.IsNullOrEmpty(this.Name))
                throw new ValidationBusinessException(ValidationMessage.NameInvalid);
        }
    }
}