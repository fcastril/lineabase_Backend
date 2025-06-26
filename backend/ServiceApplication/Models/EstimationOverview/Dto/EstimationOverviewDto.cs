namespace ServiceApplication.Dto
{
    public class EstimationOverviewDto : BaseDto
    {
        public EstimationOverviewDto() { }

        public string DiscoveryId { get; set; }
        public string Activity { get; set; }
        public string Description { get; set; }
        public string Time { get; set; }
        public string Dependency { get; set; }
        public string Category { get; set; }
        public string AutomationMigration { get; set; }
    }
}
