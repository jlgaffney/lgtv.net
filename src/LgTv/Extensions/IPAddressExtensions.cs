using System.Net;

namespace LgTv.Extensions
{
    public static class IPAddressExtensions
    {
        public static bool IsIPAddress(this string value)
        {
            return IPAddress.TryParse(value, out _);
        }
    }
}
