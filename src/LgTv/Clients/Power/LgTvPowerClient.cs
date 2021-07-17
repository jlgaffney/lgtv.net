using System;
using System.Threading.Tasks;
using LgTv.Extensions;

namespace LgTv.Power
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

            var wakeOnLan = new WakeOnLan(_ipAddress);

            await wakeOnLan.SendMagicPacket();
        }

        public async Task TurnOff()
        {
            await _connection.SendCommandAsync(new RequestMessage("ssap://system/turnOff"));
        }
    }
}
