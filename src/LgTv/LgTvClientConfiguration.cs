namespace LgTv
{
    public class LgTvClientConfiguration
    {
        public LgTvClientConfiguration(
            HostConfiguration tv,
            HostConfiguration proxy)
        {
            Tv = tv;
            Proxy = proxy;
        }

        public LgTvClientConfiguration()
        {
        }

        public HostConfiguration Tv { get; set; }

        public HostConfiguration Proxy { get; set; }
    }
}
