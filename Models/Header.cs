using System.Text.Json.Serialization;

namespace Simplz.Loki.Relay.Models
{
    public record Header
    {
        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;
        [JsonPropertyName("value")]
        public string Value { get; init; } = string.Empty;
    }
}