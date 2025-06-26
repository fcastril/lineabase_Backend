
using Domain.Common;
namespace Domain.Entities
{
    public class Problem: BaseEntity
    {
        public Problem() { }
        public Problem(string description, bool status)
        {
			Description = description;			Status = status;
        }
		public string Description { get; private set; }
		public bool Status { get; private set; }
    }
}