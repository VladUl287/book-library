using System.Text.Json.Serialization;

namespace BookLibraryApi.Configuration;

public class Config
{
    [JsonPropertyName("ISSUER")]
    public string Issuer { get; set; } = string.Empty;

    [JsonPropertyName("AUDIENCE")]
    public string Audience { get; set; } = string.Empty;

    [JsonPropertyName("ACCESSECRET")]
    public string AccessSecret { get; set; } = string.Empty;

    [JsonPropertyName("REFRESHSECRET")]
    public string RefreshSecret { get; set; } = string.Empty;

    [JsonPropertyName("LIFETIME")]
    public string LifeTime { get; set; } = string.Empty;

    [JsonPropertyName("HASH_SECRET")]
    public string HashSecret { get; set; } = string.Empty;

    [JsonPropertyName("DB_DEFAULT_CONNECTION")]
    public string DBConnection { get; set; } = string.Empty;
}