using Domain.Common;
using Domain.Entities;
namespace Domain.Entities
{
    public class CurrentTool : BaseEntity
    {
        public CurrentTool() { }
        public CurrentTool(Discovery discovery, Tool tool, string nameserver)
        {
            DiscoveryId = discovery.Id;
            Discovery = discovery;
            Tool = tool;
            NameServer = nameserver;
        }
        public string DiscoveryId { get; private set; }
        public Discovery Discovery { get; private set; }
        public Tool Tool { get; private set; }
        public string NameServer { get; private set; }
    }
}