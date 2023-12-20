namespace LgTv.Interaction;

internal class Mouse(IConnection connection) : IMouse
{
    private readonly IConnection _connection = connection ?? throw new ArgumentNullException(nameof(connection));

    public async Task<bool> Connect(string uri)
    {
        return await _connection.Connect(new Uri(uri));
    }

    public async Task SendButton(int number)
    {
        await _connection.SendMessageAsync($"type:button\nname:{number}\n\n");
    }

    public async Task SendButton(string name)
    {
        await _connection.SendMessageAsync($"type:button\nname:{name}\n\n");
    }


    public async Task Move(double dx, double dy, bool drag = false)
    {
        await _connection.SendMessageAsync($"type:move\ndx:{dx}\ndy:{dy}\ndown:{(drag ? 1 : 0)}\n\n");
    }

    public async Task Scroll(double dx, double dy)
    {
        await _connection.SendMessageAsync($"type:scroll\ndx:{dx}\ndy:{dy}\n\n");
    }

    public async Task Click()
    {
        await _connection.SendMessageAsync("type:click\n\n");
    }

    public async ValueTask DisposeAsync()
    {
        if (_connection != null)
        {
            await _connection.DisposeAsync();
        }
    }
}
