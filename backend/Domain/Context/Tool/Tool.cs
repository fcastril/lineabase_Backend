
using Domain.Common;
namespace Domain.Entities
{
    public class Tool: BaseEntity
    {
        public Tool() { }
        public Tool(string description, bool status)
        {
			Description = description;			Status = status;
        }
		public string Description { get; private set; }
		public bool Status { get; private set; }
    }
}