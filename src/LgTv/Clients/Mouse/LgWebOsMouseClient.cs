using LgTv.Connections;

namespace LgTv.Clients.Mouse;

internal class LgWebOsMouseClient(ILgTvConnection connection) : ILgWebOsMouseClient
{
    public async Task<bool> Connect(string uri)
    {
        return await connection.Connect(new Uri(uri));
    }

    public async Task SendButton(int number)
    {
        await connection.SendMessageAsync($"type:button\nname:{number}\n\n");
    }

    public async Task SendButton(string name)
    {
        await connection.SendMessageAsync($"type:button\nname:{name}\n\n");
    }


    public async Task Move(double dx, double dy, bool drag = false)
    {
        await connection.SendMessageAsync($"type:move\ndx:{dx}\ndy:{dy}\ndown:{(drag ? 1 : 0)}\n\n");
    }

    public async Task Scroll(double dx, double dy)
    {
        await connection.SendMessageAsync($"type:scroll\ndx:{dx}\ndy:{dy}\n\n");
    }

    public async Task Click()
    {
        await connection.SendMessageAsync("type:click\n\n");
    }

    public void Dispose()
    {
        connection?.Dispose();
    }
}
