using Domain.Common;
using System;

namespace Domain.Entities
{
    public class PackageMigration : BaseEntity
    {
        public PackageMigration() { }

        public PackageMigration(string discoveryId, string name, string type, string version, bool isDeleted, DateTime lastUpdate, string project, string projectId)
        {
            DiscoveryId = discoveryId;
            Name = name;
            Type = type;
            Version = version;
            IsDeleted = isDeleted;
            LastUpdate = lastUpdate;
            Project = project;
            ProjectId = projectId;
        }

        public string DiscoveryId { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Version { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime LastUpdate { get; private set; }
        public string Project { get; private set; }
        public string ProjectId { get; private set; }
    }
}
