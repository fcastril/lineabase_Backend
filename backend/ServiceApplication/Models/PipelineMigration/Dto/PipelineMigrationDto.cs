using System;

namespace ServiceApplication.Dto
{
    public class PipelineMigrationDto : BaseDto
    {
        public PipelineMigrationDto() { }

        public string DiscoveryId { get; set; }
        public string Name { get; set; }
        public string StatusLastExecution { get; set; }
        public string SourceBranch { get; set; }
        public DateTime LastExecution { get; set; }
    }
}
