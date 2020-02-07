using Insurance.Core.Domain.Interfaces.Model;

namespace Insurance.Core.Domain.Models.InputModel
{
    public class CarrierInputModel : IContractPart
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
    }
}