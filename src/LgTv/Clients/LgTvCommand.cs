namespace LgTv.Clients;

public class LgTvCommand(string uri)
{
    public LgTvCommand(
        string prefix,
        string uri)
        : this(uri)
    {
        Prefix = prefix;
    }

    public string Prefix { get; }

    public string Uri { get; } = uri;
}
