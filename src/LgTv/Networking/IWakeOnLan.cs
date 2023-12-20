namespace LgTv.Networking;

/// <summary>
/// Wake-on-LAN.
/// </summary>
public interface IWakeOnLan
{
    /// <summary>
    /// Sends the Wake-on-LAN magi packet to the specified MAC address.
    /// </summary>
    Task SendMagicPacketAsync(string macAddress);
}
