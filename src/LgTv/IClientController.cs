namespace LgTv;

public interface IClientController : IAsyncDisposable
{
    event EventHandler Connected;

    event EventHandler Disconnected;

    bool IsConnected { get; }

    IClient Client { get; }

    Task Connect(
        HostConfiguration tvHostConfiguration,
        HostConfiguration proxyHostConfiguration = null);

    Task Disconnect();
}
