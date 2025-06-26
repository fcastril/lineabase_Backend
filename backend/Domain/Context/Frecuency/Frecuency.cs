
using Domain.Common;
namespace Domain.Entities
{
    public class Frecuency: BaseEntity
    {
        public Frecuency() { }
        public Frecuency(string name, bool status)
        {
			Name = name;			Status = status;
        }
		public string Name { get; private set; }
		public bool Status { get; private set; }
    }
}