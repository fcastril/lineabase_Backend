using Domain.Common;

namespace Domain.Entities
{
    public class EstimationOverview : BaseEntity
    {
        public EstimationOverview() { }

        public EstimationOverview(string discoveryId, string activity, string description, string time, string dependency, string category, string automationMigration)
        {
            DiscoveryId = discoveryId;
            Activity = activity;
            Description = description;
            Time = time;
            Dependency = dependency;
            Category = category;
            AutomationMigration = automationMigration;
        }

        public string DiscoveryId { get; private set; }
        public string Activity {  get; private set; }
        public string Description { get; private set; }
        public string Time { get; private set; }
        public string Dependency {  get; private set; }
        public string Category { get; private set; }
        public string AutomationMigration { get; private set; }
    }
}
