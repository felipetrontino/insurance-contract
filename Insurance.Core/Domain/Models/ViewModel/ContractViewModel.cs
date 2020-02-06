using System;

namespace Insurance.Core.Domain.Models.ViewModel
{
    public class ContractViewModel
    {
        public Part From { get; set; }
        public Part To { get; set; }

        public bool Finished { get; set; }

        public class Part
        {
            public Guid Id { get; set; }
            public string Name { get; set; }

            public string Address { get; set; }

            public string Phone { get; set; }
        }
    }
}