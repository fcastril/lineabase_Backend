using Domain.Common;

namespace Domain.Entities
{
    public class ExpectedBenefit : BaseEntity
    {
        public ExpectedBenefit() { }

        public ExpectedBenefit(string category, string description, string expectedROI, double figureFrom, double figureTo, string discoveryId)
        {
            Category = category;
            Description = description;
            ExpectedROI = expectedROI;
            FigureFrom = figureFrom;
            FigureTo = figureTo;
            DiscoveryId = discoveryId;
        }
        public string DiscoveryId { get; private set; }
        public string Category { get; private set; }
        public string Description { get; private set; }
        public string ExpectedROI { get; private set; }
        public double FigureFrom { get; private set; }
        public double FigureTo { get; private set; }
    }
}
