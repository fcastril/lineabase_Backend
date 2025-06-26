
using Domain.Common;
namespace Domain.Entities
{
    public class Category: BaseEntity
    {
        public Category() { }
        public Category(string description, bool status)
        {
			Description = description;			Status = status;
        }
		public string Description { get; private set; }
		public bool Status { get; private set; }
    }
}