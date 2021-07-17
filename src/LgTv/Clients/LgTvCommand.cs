namespace LgTv.Clients
{
    public class LgTvCommand
    {
        public LgTvCommand(
            string uri)
        {
            Uri = uri;
        }

        public LgTvCommand(
            string prefix,
            string uri)
        {
            Prefix = prefix;
            Uri = uri;
        }

        public string Prefix { get; }

        public string Uri { get; }
    }
}
