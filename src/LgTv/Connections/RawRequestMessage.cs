namespace LgTv.Connections;

internal class RawRequestMessage
{
    public RawRequestMessage(RequestMessage rm, int commandCount)
    {
        var prefix = rm.Prefix ?? string.Empty;

        Id = prefix + (prefix.Length > 0 ? "_": string.Empty) + commandCount;
        Type = rm.Type ?? "request";
        Uri = rm.Uri;
        Payload = rm.Payload;
    }

    public string Id { get; set; }

    public string Type { get; set; }

    public string Uri { get; set; }

    public string Payload { get; set; }
}
