using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PipelineMigration : BaseEntity
    {
        public PipelineMigration() { }

        public PipelineMigration(string discoveryId, string name, string statusLastExecution, string sourceBranch, DateTime lastExecution)
        {
            DiscoveryId = discoveryId;
            Name = name;
            StatusLastExecution = statusLastExecution;
            SourceBranch = sourceBranch;
            LastExecution = lastExecution;
        }

        public string DiscoveryId { get; private set; }
        public string Name { get; private set; }
        public string StatusLastExecution { get; private set; }
        public string SourceBranch { get; private set; }
        public DateTime LastExecution { get; private set; }
    }
}
