using System.Net;

namespace LgTv.Extensions
{
    internal static class IPAddressExtensions
    {
        public static bool IsIPAddress(this string value)
        {
            return IPAddress.TryParse(value, out _);
        }
    }
}
