namespace ServiceApplication.Dto
{
    public class RepositoryAnalizeGenAIDto : BaseDto
    {
        public string DiscoveryId { get; set; }
        public string RepositoryId { get; set; }
        public string RepositoryName { get; set; }
        public string ValidationRule { get; set; }
        public string Status { get; set; }
        public string ProblemDetail { get; set; }
        public string Suggestion { get; set; }
    }
}
