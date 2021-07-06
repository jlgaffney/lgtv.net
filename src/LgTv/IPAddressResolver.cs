using System.Net;

namespace LgTv
{
    public class IPAddressResolver
    {
        public static string GetIPAddress(string hostname)
        {
            var hostEntry = Dns.GetHostEntry(hostname);

            // You might get more than one ip for a hostname since 
            // DNS supports more than one record
            if (hostEntry.AddressList.Length <= 0)
            {
                return null;
            }

            return hostEntry.AddressList[0].ToString();
        }
    }
}
