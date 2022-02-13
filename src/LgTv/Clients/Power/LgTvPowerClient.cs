using System;
using System.Threading.Tasks;
using LgTv.Connections;
using LgTv.Extensions;
using LgTv.Networking;

namespace LgTv.Clients.Power
{
    internal class LgTvPowerClient : ILgTvPowerClient
    {
        private readonly ILgTvConnection _connection;

        private readonly string _ipAddress;

        public LgTvPowerClient(
            ILgTvConnection connection,
            string ipAddress)
        {
            _connection = connection;

            _ipAddress = ipAddress;
        }
        
        public async Task TurnOn()
        {
            // TODO Fix, it isn't working on my TV

            if (Environment.OSVersion.IsBrowserPlatform())
            {
                throw new PlatformNotSupportedException();
            }

            var macAddress = MacAddressResolver.GetMacAddress(_ipAddress);

            await WakeOnLan.SendMagicPacket(macAddress);
        }

        public async Task TurnOff()
        {
            await _connection.SendCommandAsync(new RequestMessage(LgTvCommands.PowerOff.Uri));
        }
    }
}
