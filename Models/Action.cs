using System.Text.Json.Serialization;

namespace Simplz.Loki.Relay.Models
{
    public record Action
    {
        [JsonPropertyName("type")]
        public string Type { get; init; } = string.Empty;
        [JsonPropertyName("topicArn")]
        public string TopicArn { get; init; } = string.Empty;
        [JsonPropertyName("encoding")]
        public string Encoding { get; init; } = string.Empty;
    }
}