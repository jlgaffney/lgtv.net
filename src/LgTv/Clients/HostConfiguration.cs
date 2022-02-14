using System;

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


        public string ToUrl(
            string protocol,
            string secureProtocol)
        {
            return FormattableString.Invariant($"{(Secure ? secureProtocol : protocol) ?? string.Empty}://{Host}:{Port}");
        }
    }
}
