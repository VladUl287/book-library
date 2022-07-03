using System.Text.Json.Serialization;

namespace Common.Options
{
    public class AuthOptions
    {
        public const string Position = "Auth";

        [JsonPropertyName("ISSUER")]
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string AccessSecret { get; set; } = string.Empty;
        public string RefreshSecret { get; set; } = string.Empty;
        public int LifeTime { get; set; }
    }
}
