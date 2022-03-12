using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LgTv.Extensions;

namespace LgTv.Networking
{
    /// <summary>
    /// Resolves the MAC address of a specified IP address
    /// </summary>
    /// <remarks>
    /// Not supported on browser. Requires ARP to be installed
    /// </remarks>
    public class MacAddressResolver
    {
        /// <remarks>
        /// Not supported on browser. Requires ARP to be installed
        /// </remarks>
        public static string GetMacAddress(string ip)
        {
            if (Environment.OSVersion.IsBrowserPlatform())
            {
                throw new PlatformNotSupportedException();
            }

            if (!ip.IsIPAddress())
            {
                throw new ArgumentException("IP address is not valid");
            }

            var macIpPairs = GetAllMacAndIpAddressPairs(ip);
            return macIpPairs.FirstOrDefault(x => x.IpAddress == ip)?.MacAddress.ToUpper();
        }

        private static IEnumerable<MacIpPair> GetAllMacAndIpAddressPairs(string ip)
        {
            const string regexPattern = @"(?<ip>([0-9]{1,3}\.?){4})\s*(?<mac>([a-f0-9]{2}-?){6})";

            var arpProcess = new System.Diagnostics.Process
            {
                StartInfo =
                {
                    FileName = "arp",
                    Arguments = "-a " + ip,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            arpProcess.Start();

            var cmdOutput = arpProcess.StandardOutput.ReadToEnd();

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
