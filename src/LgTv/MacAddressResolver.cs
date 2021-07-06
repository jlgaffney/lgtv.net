using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LgTv
{
    /// <summary>
    /// Resolves the MAC address of a specified IP address
    /// </summary>
    /// <remarks>
    /// Requires ARP to be installed
    /// </remarks>
    public class MacAddressResolver
    {
        public static string GetMacAddress(string ip)
        {
            var macIpPairs = GetAllMacAndIpAddressPairs();
            return macIpPairs.FirstOrDefault(x => x.IpAddress == ip)?.MacAddress.ToUpper();
        }

        private static IEnumerable<MacIpPair> GetAllMacAndIpAddressPairs()
        {
            const string regexPattern = @"(?<ip>([0-9]{1,3}\.?){4})\s*(?<mac>([a-f0-9]{2}-?){6})";

            var pProcess = new System.Diagnostics.Process
            {
                StartInfo =
                {
                    FileName = "arp",
                    Arguments = "-a ",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            pProcess.Start();

            var cmdOutput = pProcess.StandardOutput.ReadToEnd();

            var addressPairs = new List<MacIpPair>();
            foreach (Match match in Regex.Matches(cmdOutput, regexPattern, RegexOptions.IgnoreCase))
            {
                addressPairs.Add(new MacIpPair
                {
                    MacAddress = match.Groups["mac"].Value.Replace('-', ':'),
                    IpAddress = match.Groups["ip"].Value
                });
            }

            return addressPairs;
        }

        private class MacIpPair
        {
            public string MacAddress;
            public string IpAddress;
        }
    }
}
