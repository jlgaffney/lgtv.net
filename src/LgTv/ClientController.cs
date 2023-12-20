namespace LgTv;

public class ClientController(
    ClientController.ClientFactory clientFactory)
    : IClientController
{
    public delegate IClient ClientFactory(
        HostConfiguration tvHostConfig,
        HostConfiguration proxyHostConfig = null);

    private readonly ClientFactory _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));

    private bool _isConnected;

    private bool _isDisposed;

    public event EventHandler Connected;

    public event EventHandler Disconnected;

    public bool IsConnected => Client != null && _isConnected;

    public IClient Client { get; private set; }

    public async Task Connect(
        HostConfiguration tvHostConfig,
        HostConfiguration proxyHostConfig = null)
    {
        ThrowIfDisposed();

        Client = _clientFactory(tvHostConfig, proxyHostConfig);

        // TODO Remove origin header from request, so proxy service is not required
        await Client.Connect();
        await Client.MakeHandShake();

        _isConnected = true;

        var handler = Connected;
        handler?.Invoke(null, EventArgs.Empty);
    }

    public async Task Disconnect()
    {
        ThrowIfDisposed();

        if (Client != null)
        {
            await Client.DisposeAsync();
            Client = null;
        }

        _isConnected = false;

        var handler = Disconnected;
        handler?.Invoke(null, EventArgs.Empty);
    }

    public async ValueTask DisposeAsync()
    {
        if (_isDisposed)
        {
            return;
        }

        await Disconnect();

        _isDisposed = true;
    }

    private void ThrowIfDisposed()
    {
        if (!_isDisposed)
        {
            return;
        }

        throw new ObjectDisposedException(nameof(ClientController));
    }
}
