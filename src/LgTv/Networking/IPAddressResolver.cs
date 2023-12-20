using System.Net;

namespace LgTv.Networking;

/// <inheritdoc />
/// <remarks>Not supported on browser.</remarks>
public class IPAddressResolver : IIPAddressResolver
{
    /// <inheritdoc />
    /// <remarks>Not supported on browser.</remarks>
    public async Task<string> ResolveAsync(string hostname)
    {
        var hostEntry = await Dns.GetHostEntryAsync(hostname);

        // You might get more than one ip for a hostname since 
        // DNS supports more than one record
        if (hostEntry.AddressList.Length <= 0)
        {
            return null;
        }

        return hostEntry.AddressList[0].ToString();
    }

    /// <inheritdoc />
    /// <remarks> Not supported on browser.</remarks>
    public string Resolve(string hostname)
    {
        var hostEntry = Dns.GetHostEntry(hostname);

        // You might get more than one ip for a hostname since 
        // DNS supports more than one record
        if (hostEntry.AddressList.Length <= 0)
        {
            return null;
        }

        return hostEntry.AddressList[0].ToString();
    }
}
