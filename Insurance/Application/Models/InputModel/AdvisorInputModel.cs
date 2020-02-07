using Insurance.Core.Exceptions;
using Insurance.Core.Interfaces;
using Insurance.Domain.Common;

namespace Insurance.Application.Models.InputModel
{
    public class AdvisorInputModel : ContractPartInputModel, IValidate
    {
        public string LastName { get; set; }

        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrEmpty(this.LastName))
                throw new ValidationBusinessException(ValidationMessage.LastNameInvalid);
        }
    }
}