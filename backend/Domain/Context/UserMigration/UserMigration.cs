using Domain.Common;
using System;

namespace Domain.Entities
{
    public class UserMigration : BaseEntity
    {
        public UserMigration() { }

        public UserMigration(string discoveryId, string name, string email, string role, string tool, DateTime lastAccess)
        {
            DiscoveryId = discoveryId;
            Name = name;
            Email = email;
            Role = role;
            Tool = tool;
            LastAccess = lastAccess;
        }

        public string DiscoveryId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Role { get; private set; }
        public string Tool { get; private set; }
        public DateTime LastAccess { get; private set; }
    }
}
