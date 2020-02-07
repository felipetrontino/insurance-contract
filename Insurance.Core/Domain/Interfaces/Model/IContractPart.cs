namespace Insurance.Core.Domain.Interfaces.Model
{
    public interface IContractPart
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
    }
}