using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LgTv.Extensions;

namespace LgTv.Networking
{
    /// <remarks>Not supported on browser</remarks>
    internal class WakeOnLan
    {
        public static async Task SendMagicPacket(string macAddress)
        {
            if (Environment.OSVersion.IsBrowserPlatform())
            {
                throw new PlatformNotSupportedException();
            }

            macAddress = Regex.Replace(macAddress ?? string.Empty, "[-|:]", string.Empty);

            if (string.IsNullOrWhiteSpace(macAddress))
            {
                throw new ArgumentException("MAC address not specified");
            }

            var magicPacket = BuildMagicPacket(macAddress);
            foreach (var networkInterface in NetworkInterface.GetAllNetworkInterfaces().Where(n =>
                n.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                n.OperationalStatus == OperationalStatus.Up))
            {
                var iPInterfaceProperties = networkInterface.GetIPProperties();
                foreach (var multicastIPAddressInformation in iPInterfaceProperties.MulticastAddresses)
                {
                    var multicastIpAddress = multicastIPAddressInformation.Address;

                    UnicastIPAddressInformation unicastIPAddressInformation = null;
                    if (multicastIpAddress.ToString().StartsWith("ff02::1%", StringComparison.OrdinalIgnoreCase)) // Ipv6: All hosts on LAN (with zone index)
                    {
                        unicastIPAddressInformation = iPInterfaceProperties.UnicastAddresses.FirstOrDefault(u =>
                            u.Address.AddressFamily == AddressFamily.InterNetworkV6 && !u.Address.IsIPv6LinkLocal);
                    }
                    else if (multicastIpAddress.ToString().Equals("224.0.0.1")) // Ipv4: All hosts on LAN
                    {
                        unicastIPAddressInformation = iPInterfaceProperties.UnicastAddresses.FirstOrDefault(u =>
                            u.Address.AddressFamily == AddressFamily.InterNetwork && !iPInterfaceProperties.GetIPv4Properties().IsAutomaticPrivateAddressingActive);
                    }

                    if (unicastIPAddressInformation == null)
                    {
                        continue;
                    }

                    using (var client = new UdpClient(new IPEndPoint(unicastIPAddressInformation.Address, 0)))
                    {
                        await client.SendAsync(magicPacket, magicPacket.Length, new IPEndPoint(multicastIpAddress, 9));
                    }
                }
            }
        }

        private static byte[] BuildMagicPacket(string macAddress)
        {
            macAddress = Regex.Replace(macAddress, "[: -]", "");
            var macBytes = HexStringToByteArray(macAddress);

            var header = Enumerable.Repeat((byte) 0xff, 6); //First 6 times 0xff
            var data = Enumerable.Repeat(macBytes, 16).SelectMany(m => m); // then 16 times MacAddress
            return header.Concat(data).ToArray();
        }


        private static byte[] HexStringToByteArray(string hex)
        {
            if (hex.Length % 2 == 1)
            {
                throw new Exception("The binary key cannot have an odd number of digits");
            }

            var arr = new byte[hex.Length >> 1];

            for (var i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte) ((GetHexVal(hex[i << 1]) << 4) + GetHexVal(hex[(i << 1) + 1]));
            }

            return arr;
        }

        private static int GetHexVal(char hex)
        {
            var val = (int) hex;

            //For uppercase A-F letters:
            //return val - (val < 58 ? 48 : 55);
            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);
            //Or the two combined, but a bit slower:
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }
    }
}
