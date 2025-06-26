using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class GithubAppBaseRequest
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}