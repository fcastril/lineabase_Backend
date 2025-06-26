namespace ServiceApplication.Dto
{
    public class BenefitDto : BaseDto
    {
        public BenefitDto() { }

        public string DiscoveryId { get; set; }
        //public CategoryDto Category { get; set; }
        public string Category { get; set; }
        public string CurrentProblem { get; set; }
        public string CurrentTool { get; set; }
        public string NewTool { get; set; }
        public string MigrationBenefit { get; set; }
        public bool IsRelevant { get; set; }

    }
}
