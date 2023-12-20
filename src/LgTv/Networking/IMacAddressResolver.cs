namespace LgTv.Networking;

/// <summary>
/// Resolves the MAC address of a specified IP address
/// </summary>
public interface IMacAddressResolver
{
    /// <summary>
    /// Resolves the MAC address of the specified IP address
    /// </summary>
    public Task<string> ResolveAsync(string ipAddress);
}
