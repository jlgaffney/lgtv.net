namespace LgTv.Features.Inputs;

internal class InputClient(IConnection connection) : IInputClient
{
    private readonly IConnection _connection = connection ?? throw new ArgumentNullException(nameof(connection));

    public async Task<Input> GetInput(string id)
    {
        return (await GetInputs()).FirstOrDefault(x => string.Equals(id, x.Id, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<IEnumerable<Input>> GetInputs()
    {
        var requestMessage = new RequestMessage(Commands.GetInputs.Prefix, Commands.GetInputs.Uri);
        var response = await _connection.SendCommandAsync(requestMessage);

        var inputs = new List<Input>();
        foreach (var device in response.devices)
        {
            inputs.Add(new Input
            {
                Id = device.id,
                Label = device.label,
                Icon = device.icon,
                Connected = (bool)device.connected
            });
        }

        return inputs;
    }

    public async Task SetInput(string id)
    {
        var requestMessage = new RequestMessage(Commands.SetInput.Uri, new { inputId = id });
        await _connection.SendCommandAsync(requestMessage);
    }
}
