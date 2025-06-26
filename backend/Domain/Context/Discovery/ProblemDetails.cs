using Domain.Common;
using Domain.Entities;
namespace Domain.Entities
{
    public class ProblemDetails : BaseEntity
    {
        public ProblemDetails() { }
        public ProblemDetails(Discovery discovery, Category category, Problem problem, int impact, Frecuency frecuency, string description)
        {
            Discovery = discovery;
            Category = category;
            Problem = problem;
            Impact = impact;
            Frecuency = frecuency;
            Description = description;
        }
        public Discovery Discovery { get; private set; }
        public Category Category { get; private set; }
        public Problem Problem { get; private set; }
        public int Impact { get; private set; }
        public Frecuency Frecuency { get; private set; }
        public string Description { get; private set; }
    }
}