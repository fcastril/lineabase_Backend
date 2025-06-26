using Domain.Common;
using System;

namespace Domain.Entities
{
    public class RepositoryMigration : BaseEntity
    {
        public RepositoryMigration() { }

        public RepositoryMigration(string discoveryId, string assessment, string name, string description, long size, int branchCount, bool activePullRequest, DateTime lastCommit, bool isDisabled, string defaultBranch, string branchs, string frecuencyCommits, string filesExtensions)
        {
            DiscoveryId = discoveryId;
            Assessment = assessment;
            Name = name;
            Description = description;
            Size = size;
            BranchCount = branchCount;
            ActivePullRequest = activePullRequest;
            LastCommit = lastCommit;
            IsDisabled = isDisabled;
            DefaultBranch = defaultBranch;
            Branchs = branchs;
            FrecuencyCommits = frecuencyCommits;
            FileExtensions = filesExtensions;
        }

        public string DiscoveryId { get; private set; }
        public string Assessment {  get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public long Size { get; private set; }
        public int BranchCount { get; private set; }
        public bool ActivePullRequest {  get; private set; }
        public DateTime LastCommit {  get; private set; }
        public bool IsDisabled { get; private set; }
        public string DefaultBranch { get; private set; }
        public string Branchs { get; private set; }
        public string FrecuencyCommits { get; private set; }
        public string FileExtensions { get; private set; }
    }
}
