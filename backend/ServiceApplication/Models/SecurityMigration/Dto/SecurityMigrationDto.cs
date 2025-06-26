namespace ServiceApplication.Dto
{
    public class SecurityMigrationDto : BaseDto
    {
        public SecurityMigrationDto() { }

        public string DiscoveryId { get; set; }
        public string ProjectId { get; set; }
        public string Project { get; set; }
        public string TeamId { get; set; }
        public string Team { get; set; }
        public string User { get; set; }
        public string UserEmail { get; set; }
    }
}
