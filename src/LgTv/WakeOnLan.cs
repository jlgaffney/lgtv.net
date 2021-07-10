/*
 * From https://www.fluxbytes.com/csharp/wake-lan-wol-c/
 */

using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LgTv.Extensions;

namespace LgTv
{
    /// <remarks>Not supported on browser</remarks>
    internal class WakeOnLan
    {
        private const int MacAddressRepeatCount = 16;

        private readonly string _macAddress;

        /// <remarks>Not supported on browser</remarks>
        public WakeOnLan(
            string ipAddress)
        {
            if (Environment.OSVersion.IsBrowserPlatform())
            {
                throw new PlatformNotSupportedException();
            }

            _macAddress = Regex.Replace(MacAddressResolver.GetMacAddress(ipAddress), "[-|:]", "");
        }

        public async Task Wake()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
            {
                EnableBroadcast = true
            };

            var payloadIndex = 0;

            // The magic packet is a broadcast frame containing anywhere within its payload 6 bytes of all 255 (FF FF FF FF FF FF in hexadecimal),
            // followed by sixteen repetitions of the target computer's 48-bit MAC address, for a total of 102 bytes.
            var payload = new byte[1024];    // Our packet that we will be broadcasting

            // Add 6 bytes with value 255 (FF) in our payload
            for (var i = 0; i < 6; i++)
            {
                payload[payloadIndex] = 255;
                payloadIndex++;
            }

            // Repeat the device MAC address sixteen times
            for (var j = 0; j < MacAddressRepeatCount; j++)
            {
                for (var k = 0; k < _macAddress.Length; k += 2)
                {
                    var s = _macAddress.Substring(k, 2);
                    payload[payloadIndex] = byte.Parse(s, NumberStyles.HexNumber);
                    payloadIndex++;
                }
            }

            await Task.Run(() =>
            {
                // Broadcast our packet
                socket.SendTo(payload, new IPEndPoint(IPAddress.Parse("255.255.255.255"), 0));
                socket.Close(10000);
            });
        }


        //we derive our class from a standart one
        /*public class WOLClass : UdpClient
        {
            public WOLClass() : base()
            { }

            public void SetClientToBroadcastMode()
            {
                if (this.Active)
                {
                    Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 0);
                }
            }
        }

        //now use this class
        //MAC_ADDRESS should  look like '013FA049'
        private void WakeFunction(string MAC_ADDRESS)
        {
            WOLClass client = new WOLClass();
            client.Connect(new
                    IPAddress(0xffffffff),  //255.255.255.255  i.e broadcast
                0x2fff); // port=12287 let's use this one 
            client.SetClientToBroadcastMode();
            //set sending bites
            int counter = 0;
            //buffer to be send
            byte[] bytes = new byte[1024];   // more than enough :-)
            //first 6 bytes should be 0xFF
            for (int y = 0; y < 6; y++)
                bytes[counter++] = 0xFF;
            //now repeate MAC 16 times
            for (int y = 0; y < 16; y++)
            {
                int i = 0;
                for (int z = 0; z < 6; z++)
                {
                    bytes[counter++] =
                        byte.Parse(MAC_ADDRESS.Substring(i, 2), NumberStyles.HexNumber);
                    i += 2;
                }
            }

            //now send wake up packet
            int reterned_value = client.Send(bytes, 1024);
        }*/
    }
}
