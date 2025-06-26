namespace ServiceApplication.Dto
{
    public class ExpectedBenefitDto : BaseDto
    {
        public ExpectedBenefitDto() { }

        public string DiscoveryId { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string ExpectedROI { get; set; }
        public double FigureFrom { get; set; }
        public double FigureTo { get; set; }
    }
}
