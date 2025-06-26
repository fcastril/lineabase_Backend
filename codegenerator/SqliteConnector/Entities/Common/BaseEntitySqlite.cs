using System.Text.Json.Serialization;

namespace SqliteConnector;

public class BaseEntitySqlite
{
    public BaseEntitySqlite()
    {
    }

    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("code")]
    public string Code { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; }
}
