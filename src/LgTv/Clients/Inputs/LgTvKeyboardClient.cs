using LgTv.Connections;

namespace LgTv.Clients.Inputs;

public class LgTvKeyboardClient(ILgTvConnection connection) : ILgTvKeyboardClient
{
    public async Task InsertText(string text, int replace = 0)
    {
        var requestMessage = new RequestMessage(LgTvCommands.InsertText.Uri, new { text, replace });
        await connection.SendCommandAsync(requestMessage);
    }

    public async Task DeleteCharacters(int count = 1)
    {
        var requestMessage = new RequestMessage(LgTvCommands.DeleteCharacters.Uri, new { count });
        await connection.SendCommandAsync(requestMessage);
    }

    public async Task SendEnterKey()
    {
        var requestMessage = new RequestMessage(LgTvCommands.SendEnterKey.Uri);
        await connection.SendCommandAsync(requestMessage);
    }
}
