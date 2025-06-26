using System.Text.Json.Serialization;

namespace Domain.Entity
{
    public class GithubAppRequest
    {
        [JsonPropertyName("clientId")]
        public string ClientId { get; set; } = string.Empty;
        [JsonPropertyName("clientSecret")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string ClientSecret { get; set; }
        [JsonPropertyName("code")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Code { get; set; } = string.Empty;
        [JsonPropertyName("refreshToken")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string RefreshToken { get; set; } = string.Empty;
        [JsonPropertyName("redirectUri")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string RedirectUri { get; set; } = string.Empty;
    }
}