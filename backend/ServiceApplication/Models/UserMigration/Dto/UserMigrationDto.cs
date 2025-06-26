using System;

namespace ServiceApplication.Dto
{
    public class UserMigrationDto : BaseDto
    {
        public UserMigrationDto() { }

        public string DiscoveryId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Tool { get; set; }
        public DateTime LastAccess { get; set; }
    }
}
