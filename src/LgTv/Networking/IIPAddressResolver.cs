namespace LgTv.Networking;

/// <summary>
/// Resolves the IP address of a specified hostname.
/// </summary>
public interface IIPAddressResolver
{
    /// <summary>
    /// Resolves the IP address of the specified hostname.
    /// </summary>
    /// <param name="hostname">The hostname.</param>
    /// <returns>The IP address if resolved, otherwise <see langword="null" /></returns>
    Task<string> ResolveAsync(string hostname);

    /// <summary>
    /// Resolves the IP address of the specified hostname.
    /// </summary>
    /// <param name="hostname">The hostname.</param>
    /// <returns>The IP address if resolved, otherwise <see langword="null" /></returns>
    string Resolve(string hostname);
}
