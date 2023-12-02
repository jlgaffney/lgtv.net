using LgTv.Connections;
using LgTv.Extensions;
using LgTv.Networking;

namespace LgTv.Clients.Power;

internal class LgTvPowerClient(ILgTvConnection connection, string ipAddress) : ILgTvPowerClient
{
    public async Task TurnOn()
    {
        if (Environment.OSVersion.IsBrowserPlatform())
        {
            throw new PlatformNotSupportedException();
        }

        var macAddress = MacAddressResolver.GetMacAddress(ipAddress);

        await WakeOnLan.SendMagicPacket(macAddress);
    }

    public async Task TurnOff()
    {
        await connection.SendCommandAsync(new RequestMessage(LgTvCommands.PowerOff.Uri));
    }
}
