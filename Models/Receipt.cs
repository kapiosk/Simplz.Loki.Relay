using System.Text.Json.Serialization;
using System;

namespace Simplz.Loki.Relay.Models
{
    public record Receipt
    {
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; init; }
        [JsonPropertyName("processingTimeMillis")]
        public int ProcessingTimeMillis { get; init; }
        [JsonPropertyName("recipients")]
        public string[] Recipients { get; init; } = Array.Empty<string>();
        [JsonPropertyName("spamVerdict")]
        public Status SpamVerdict { get; init; } = new();
        [JsonPropertyName("virusVerdict")]
        public Status VirusVerdict { get; init; } = new();
        [JsonPropertyName("spfVerdict")]
        public Status SpfVerdict { get; init; } = new();
        [JsonPropertyName("dkimVerdict")]
        public Status DkimVerdict { get; init; } = new();
        [JsonPropertyName("dmarcVerdict")]
        public Status DmarcVerdict { get; init; } = new();
        [JsonPropertyName("action")]
        public Action Action { get; init; } = new();
    }
}