using Domain.Common;

namespace Domain.Entities
{
    public class RepositoryAnalizeGenAI : BaseEntity
    {
        public RepositoryAnalizeGenAI() { }

        public RepositoryAnalizeGenAI(string discoveryId, string repositoryId, string repositoryName, string validationRule, string status, string problemDetail, string suggestion)
        {
            DiscoveryId = discoveryId;
            RepositoryId = repositoryId;
            RepositoryName = repositoryName;
            ValidationRule = validationRule;
            Status = status;
            ProblemDetail = problemDetail;
            Suggestion = suggestion;
        }

        public string DiscoveryId { get; private set; }
        public string RepositoryId { get; private set; }
        public string RepositoryName { get; private set; }
        public string ValidationRule { get; private set; }
        public string Status { get; private set; }
        public string ProblemDetail { get; private set; }
        public string Suggestion { get; private set; }
    }
}
