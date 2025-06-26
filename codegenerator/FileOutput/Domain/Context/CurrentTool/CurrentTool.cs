
using Domain.Common;
namespace Domain.Entities
{
    public class CurrentTool: BaseEntity
    {
        public CurrentTool() { }
        public CurrentTool(Discovery discovery, Tool tool, Area area, string nameserver)
        {
			Discovery = discovery;			Tool = tool;			Area = area;			NameServer = nameserver;
        }
		public Discovery Discovery { get; private set; }
		public Tool Tool { get; private set; }
		public Area Area { get; private set; }
		public string NameServer { get; private set; }
    }
}