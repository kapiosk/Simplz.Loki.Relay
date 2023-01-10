using System.Text.Json.Serialization;
using System;

namespace Simplz.Loki.Relay.Models
{
    public record Commonheaders
    {
        [JsonPropertyName("returnPath")]
        public string ReturnPath { get; init; } = string.Empty;
        [JsonPropertyName("from")]
        public string[] From { get; init; } = Array.Empty<string>();
        [JsonPropertyName("date")]
        public string Date { get; init; } = string.Empty;
        [JsonPropertyName("to")]
        public string[] To { get; init; } = Array.Empty<string>();
        [JsonPropertyName("messageId")]
        public string MessageId { get; init; } = string.Empty;
        [JsonPropertyName("subject")]
        public string Subject { get; init; } = string.Empty;
    }
}