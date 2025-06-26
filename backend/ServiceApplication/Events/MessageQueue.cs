using System;

namespace ServiceApplication.Events
{
    public class MessageQueue
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string DiscoveryId { get; set; } = string.Empty;
        public string Tool { get; set; } = string.Empty;
    }
}
