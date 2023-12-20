using System.Diagnostics;
using System.Text.RegularExpressions;
using LgTv.Extensions;

namespace LgTv.Networking;

/// <inheritdoc />
/// <remarks>Not supported on browser. Requires ARP to be installed.</remarks>
public class MacAddressResolver : IMacAddressResolver
{
    /// <inheritdoc />
    /// <remarks>Not supported on browser. Requires ARP to be installed.</remarks>
    public async Task<string> ResolveAsync(string ipAddress)
    {
        if (Environment.OSVersion.IsBrowserPlatform())
        {
            throw new PlatformNotSupportedException();
        }

        if (!ipAddress.IsIPAddress())
        {
            throw new ArgumentException("IP address is not valid");
        }

        var macIpPairs = await GetAllMacAndIpAddressPairsAsync(ipAddress);
        return macIpPairs.FirstOrDefault(x => x.IpAddress == ipAddress)?.MacAddress.ToUpper();
    }

    private static async Task<IEnumerable<MacIpPair>> GetAllMacAndIpAddressPairsAsync(string ipAddress)
    {
        const string regexPattern = @"(?<ip>([0-9]{1,3}\.?){4})\s*(?<mac>([a-f0-9]{2}-?){6})";

        var arpProcess = new Process
        {
            StartInfo =
            {
                FileName = "arp",
                Arguments = "-a " + ipAddress,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };
        arpProcess.Start();
        arpProcess.WaitForExit();

        var cmdOutput = await arpProcess.StandardOutput.ReadToEndAsync();

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
