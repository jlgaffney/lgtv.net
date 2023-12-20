namespace LgTv;

internal class Command(string uri)
{
    public Command(
        string prefix,
        string uri)
        : this(uri)
    {
        Prefix = prefix;
    }

    public string Prefix { get; }

    public string Uri { get; } = uri;
}
