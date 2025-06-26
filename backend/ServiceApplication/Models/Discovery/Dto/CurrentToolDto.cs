using System.Text.Json.Serialization;

namespace ServiceApplication.Dto
{
    public class CurrentToolDto : BaseDto
    {
        public DiscoveryDto Discovery { get; set; }
        public string DiscoveryId { get; set; }
        public ToolDto Tool { get; set; }
        public string NameServer { get; set; }

        public CurrentToolDto()
        {


        }

    }
}