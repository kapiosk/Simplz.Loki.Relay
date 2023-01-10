using System.Text.Json.Serialization;

namespace Simplz.Loki.Relay.Models
{
    public record SQSMessage
    {
        [JsonPropertyName("notificationType")]
        public string NotificationType { get; init; } = string.Empty;
        [JsonPropertyName("mail")]
        public Mail Mail { get; init; } = new();
        [JsonPropertyName("receipt")]
        public Receipt Receipt { get; init; } = new();
        [JsonPropertyName("content")]
        public string Content { get; init; } = string.Empty;
    }
}