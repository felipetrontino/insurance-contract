using Insurance.Domain.Interfaces.Model;

namespace Insurance.Application.Models.InputModel
{
    public class AdvisorInputModel : IContractPart
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
    }
}