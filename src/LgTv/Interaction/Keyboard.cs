namespace LgTv.Interaction;

public class Keyboard(IConnection connection) : IKeyboard
{
    private readonly IConnection _connection = connection ?? throw new ArgumentNullException(nameof(connection));

    public async Task InsertText(string text, int replace = 0)
    {
        var requestMessage = new RequestMessage(Commands.InsertText.Uri, new { text, replace });
        await _connection.SendCommandAsync(requestMessage);
    }

    public async Task DeleteCharacters(int count = 1)
    {
        var requestMessage = new RequestMessage(Commands.DeleteCharacters.Uri, new { count });
        await _connection.SendCommandAsync(requestMessage);
    }

    public async Task SendEnterKey()
    {
        var requestMessage = new RequestMessage(Commands.SendEnterKey.Uri);
        await _connection.SendCommandAsync(requestMessage);
    }
}
