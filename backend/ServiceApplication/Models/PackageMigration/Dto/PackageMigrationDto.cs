using System;

namespace ServiceApplication.Dto
{
    public class PackageMigrationDto : BaseDto
    {
        public PackageMigrationDto() { }

        public string DiscoveryId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Version { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Project { get; set; }
        public string ProjectId { get; set; }
    }
}
