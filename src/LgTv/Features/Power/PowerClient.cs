using LgTv.Extensions;
using LgTv.Networking;

namespace LgTv.Features.Power;

internal class PowerClient(
    IConnection connection,
    IMacAddressResolver macAddressResolver,
    IWakeOnLan wakeOnLan,
    string ipAddress) : IPowerClient
{
    private readonly IConnection _connection = connection ?? throw new ArgumentNullException(nameof(connection));
    private readonly IMacAddressResolver _macAddressResolver = macAddressResolver ?? throw new ArgumentNullException(nameof(macAddressResolver));
    private readonly IWakeOnLan _wakeOnLan = wakeOnLan ?? throw new ArgumentNullException(nameof(wakeOnLan));
    private readonly string _ipAddress = ipAddress ?? throw new ArgumentNullException(nameof(ipAddress));

    public async Task TurnOn()
    {
        if (Environment.OSVersion.IsBrowserPlatform())
        {
            throw new PlatformNotSupportedException();
        }

        var macAddress = await _macAddressResolver.ResolveAsync(_ipAddress);

        await _wakeOnLan.SendMagicPacketAsync(macAddress);
    }

    public async Task TurnOff()
    {
        await _connection.SendCommandAsync(new RequestMessage(Commands.PowerOff.Uri));
    }
}
