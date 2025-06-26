using Domain.Common;

namespace Domain.Entities
{
    public class Benefit : BaseEntity
    {
        public Benefit() { }

        public Benefit(string category, string currentProblem, string currentTool, string newTool, string migrationBenefit, bool isRelevant, string discoveryId)
        {
            Category = category;
            CurrentProblem = currentProblem;
            CurrentTool = currentTool;
            NewTool = newTool;
            MigrationBenefit = migrationBenefit;
            IsRelevant = isRelevant;
            DiscoveryId = discoveryId;
        }
        public string DiscoveryId { get; private set; }
        public string Category { get; private set; }
        public string CurrentProblem { get; private set; }
        public string CurrentTool { get; private set; }
        public string NewTool { get; private set; }
        public string MigrationBenefit { get; private set; }
        public bool IsRelevant { get; private set; }
    }
}
