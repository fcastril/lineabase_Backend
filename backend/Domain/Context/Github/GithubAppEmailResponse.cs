using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class GithubAppEmailResponse
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("primary")]
        public bool Primary { get; set; }

        [JsonPropertyName("verified")]
        public bool Verified { get; set; }

        [JsonPropertyName("visibility")]
        public string Visibility { get; set; }
    }
}