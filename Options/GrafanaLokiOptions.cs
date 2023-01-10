using Serilog.Sinks.Grafana.Loki;

namespace Simplz.Loki.Relay.Options
{
    public record GrafanaLokiOptions
    {
        public string Uri { get; init; } = string.Empty;
        //public LokiLabel[] LokiLabels { get; init; } = Array.Empty<LokiLabel>();
        public string LokiLabelsKey { get; init; } = string.Empty;
        public string LokiLabelsValue { get; init; } = string.Empty;
        public LokiCredentials LokiCredentials { get; init; } = new();
    }
}