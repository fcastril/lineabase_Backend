
using Domain.Common;
namespace Domain.Entities
{
    public class Area: BaseEntity
    {
        public Area() { }
        public Area(string description, bool status)
        {
			Description = description;			Status = status;
        }
		public string Description { get; private set; }
		public bool Status { get; private set; }
    }
}