using System.Text.Json.Serialization;
using System;

namespace Simplz.Loki.Relay.Models
{
    public record Mail
    {
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; init; }
        [JsonPropertyName("source")]
        public string Source { get; init; } = string.Empty;
        [JsonPropertyName("messageId")]
        public string MessageId { get; init; } = string.Empty;
        [JsonPropertyName("destination")]
        public string[] Destination { get; init; } = Array.Empty<string>();
        [JsonPropertyName("headersTruncated")]
        public bool HeadersTruncated { get; init; }
        [JsonPropertyName("headers")]
        public Header[] Headers { get; init; } = Array.Empty<Header>();
        [JsonPropertyName("commonHeaders")]
        public Commonheaders CommonHeaders { get; init; } = new();
    }
}