using System;

namespace ServiceApplication.Dto
{
    public class RepositoryMigrationDto : BaseDto
    {
        public RepositoryMigrationDto() { }

        public string DiscoveryId { get; set; }
        public string Assessment { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Size { get; set; }
        public int BranchCount { get; set; }
        public bool ActivePullRequest { get; set; }
        public DateTime LastCommit { get; set; }
        public bool IsDisable { get; set; }
        public string DefaultBranch { get; set; }
        public string Branchs { get; set; }
        public string FrecuencyCommits { get; set; }
        public string FileExtensions { get; set; }
    }
}
