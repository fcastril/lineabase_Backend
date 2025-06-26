
using Domain.Common;
namespace Domain.Entities
{
    public class Sector: BaseEntity
    {
        public Sector() { }
        public Sector(string description, bool status)
        {
			Description = description;			Status = status;
        }
		public string Description { get; private set; }
		public bool Status { get; private set; }
    }
}