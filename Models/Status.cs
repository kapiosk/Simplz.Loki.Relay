using System.Text.Json.Serialization;

namespace Simplz.Loki.Relay.Models
{
    public record Status
    {
        [JsonPropertyName("status")]
        public string Value { get; init; } = string.Empty;
    }
}