namespace Simplz.Loki.Relay.Models
{
    public record PostMessage
    {
        public string Label { get; init; } 
        public string EmailContent { get; init; }
    }
}