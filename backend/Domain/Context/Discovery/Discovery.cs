
using Domain.Common;
using System;
namespace Domain.Entities
{
    public class Discovery : BaseEntity
    {
        public Discovery()
        {
        }
        public Discovery(Customer customer, DateTimeOffset? date, string description)
        {
            Customer = customer;
            Date = date;
            Description = description;
        }
        public Customer Customer { get; private set; }
        public DateTimeOffset? Date { get; private set; }
        public string Description { get; private set; }
        public string StatusBenefit { get; private set; }
    }
}