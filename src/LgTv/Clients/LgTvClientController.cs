namespace LgTv.Clients;

public class LgTvClientController(
    LgTvClientController.ClientFactory clientFactory)
    : ILgTvClientController
{
    public delegate ILgTvClient ClientFactory(
        HostConfiguration tvHostConfig,
        HostConfiguration proxyHostConfig = null);

    private bool _isConnected;

    private bool _isDisposed;

    public event EventHandler Connected;

    public event EventHandler Disconnected;

    public bool IsConnected => Client != null && _isConnected;

    public ILgTvClient Client { get; private set; }

    public async Task Connect(
        HostConfiguration tvHostConfig,
        HostConfiguration proxyHostConfig = null)
    {
        ThrowIfDisposed();

        Client = clientFactory(tvHostConfig, proxyHostConfig);

        // TODO Remove origin header from request, so proxy service is not required
        await Client.Connect();
        await Client.MakeHandShake();

        _isConnected = true;

        var handler = Connected;
        handler?.Invoke(null, EventArgs.Empty);
    }

    public void Disconnect()
    {
        ThrowIfDisposed();

        Client?.Dispose();
        Client = null;

        _isConnected = false;

        var handler = Disconnected;
        handler?.Invoke(null, EventArgs.Empty);
    }

    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        Disconnect();

        _isDisposed = true;
    }

    private void ThrowIfDisposed()
    {
        if (!_isDisposed)
        {
            return;
        }

        throw new ObjectDisposedException(nameof(LgTvClientController));
    }
}
