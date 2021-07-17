namespace LgTv.Clients
{
    public class HostConfiguration
    {
        public HostConfiguration(
            bool secure,
            string host,
            int port)
        {
            Secure = secure;
            Host = host;
            Port = port;
        }

        public HostConfiguration()
        {
        }

        public bool Secure { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }
    }
}
