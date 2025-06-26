using Domain.Common;

namespace Domain.Entities
{
    public class SecurityMigration : BaseEntity
    {
        public SecurityMigration(){ }

        public SecurityMigration(string discoveryId, string projectId, string project, string teamId, string team, string user, string userEmail)
        {
            DiscoveryId = discoveryId;
            ProjectId = projectId;
            Project = project;
            TeamId = teamId;
            Team = team;
            User = user;
            UserEmail = userEmail;
        }

        public string DiscoveryId { get; private set; }
        public string ProjectId { get; private set; }
        public string Project { get; private set; }
        public string TeamId { get; private set; }
        public string Team { get; private set; }
        public string User { get; private set; }
        public string UserEmail { get; private set; }
    }
}
