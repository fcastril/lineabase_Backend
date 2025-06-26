using Domain.Common;

namespace Domain.Entities
{
    public class ConnectTool : BaseEntity
    {
        public ConnectTool() { }

        public ConnectTool(string discoveryId, string organization, string pat) 
        { 
            DiscoveryId = discoveryId;
            Organization = organization;
            PAT = pat;
        }

        public string DiscoveryId { get; private set; }
        public string Organization { get; private set; }
        public string PAT { get; private set; }
    }
}
