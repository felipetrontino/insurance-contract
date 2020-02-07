namespace Insurance.Domain.Entities
{
    public class Advisor : ContractPart
    {
        public string LastName { get; set; }

        public HealthStatus HealthStatus { get; set; }
    }
}