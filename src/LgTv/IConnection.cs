namespace LgTv;

public interface IConnection : IAsyncDisposable
{
    Task<bool> Connect(Uri uri);

    Task SendMessageAsync(string message);

    Task<dynamic> SendCommandAsync(string message);

    Task<dynamic> SendCommandAsync(RequestMessage message);

    Task CloseAsync();
}
