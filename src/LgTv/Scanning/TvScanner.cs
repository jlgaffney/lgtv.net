using System.Text.RegularExpressions;
using System.Web;
using LgTv.Extensions;
using Rssdp;

namespace LgTv.Scanning;

public class TvScanner : ITvScanner
{
    private const char HeaderSeparator = ':';
    private const string UuidHeaderPrefix = "uuid";
    private const string LgDeviceType = "urn:schemas-upnp-org:device:MediaRenderer:1";
    private const string LgDeviceName = "DLNADeviceName.lge.com";

    private static readonly string UuidRegexPattern = UuidHeaderPrefix + HeaderSeparator + "(.*?)" + HeaderSeparator;

    public async Task<IEnumerable<Device>> GetDevices()
    {
        var devices = new List<Device>();

        using var deviceLocator = new SsdpDeviceLocator();

        var foundDevices = await deviceLocator.SearchAsync(LgDeviceType);

        foreach (var foundDevice in foundDevices)
        {
            var ipAddress = foundDevice.DescriptionLocation.Host;

            if (string.IsNullOrEmpty(ipAddress) || !ipAddress.IsIPAddress())
            {
                continue;
            }

            var id = Regex.Match(foundDevice.Usn ?? string.Empty, UuidRegexPattern).Value;

            if (id.StartsWith(UuidHeaderPrefix))
            {
                id = id.Substring(4, id.Length - 4);
            }

            id = id.Trim(':');

            if (string.IsNullOrWhiteSpace(id))
            {
                continue;
            }

            var deviceNames = foundDevice.ResponseHeaders
                .FirstOrDefault(x => x.Key == LgDeviceName)
                .Value?.ToArray();

            if (deviceNames == null || deviceNames.Length == 0)
            {
                continue;
            }

            var deviceName = deviceNames.FirstOrDefault(x =>
                !string.IsNullOrWhiteSpace(x));

            if (deviceName == null)
            {
                continue;
            }

            deviceName = HttpUtility.UrlDecode(deviceName);
                
            devices.Add(new Device(id, deviceName, ipAddress));
        }

        return devices;
    }
}
