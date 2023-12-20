namespace LgTv.Features.Channels;

internal class ChannelClient(IConnection connection) : IChannelClient
{
    private readonly IConnection _connection = connection ?? throw new ArgumentNullException(nameof(connection));

    public async Task<Channel> GetChannel(string id)
    {
        return (await GetChannels()).FirstOrDefault(x => string.Equals(id, x.Id, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<IEnumerable<Channel>> GetChannels()
    {
        var requestMessage = new RequestMessage(Commands.GetChannels.Prefix, Commands.GetChannels.Uri);
        var response = await _connection.SendCommandAsync(requestMessage);

        var channels = new List<Channel>();
        foreach (var channel in response.channelList)
        {
            channels.Add(new Channel
            {
                Id = channel.channelId,
                Name = channel.channelName,
                Number = int.Parse((string)channel.channelNumber)
            });
        }

        return channels.OrderBy(x => x.Number);
    }

    public async Task<Channel> GetCurrentChannel()
    {
        var requestMessage = new RequestMessage(Commands.GetCurrentChannel.Prefix, Commands.GetCurrentChannel.Uri);
        var response = await _connection.SendCommandAsync(requestMessage);

        if (response.channelId == null)
        {
            return null;
        }

        return new Channel
        {
            Id = response.channelId,
            Name = response.channelName,
            Number = int.Parse((string)response.channelNumber)
        };
    }

    public async Task ChannelUp()
    {
        await _connection.SendCommandAsync(new RequestMessage(Commands.SetChannelUp.Prefix, Commands.SetChannelUp.Uri));
    }

    public async Task ChannelDown()
    {
        await _connection.SendCommandAsync(new RequestMessage(Commands.SetChannelDown.Prefix, Commands.SetChannelDown.Uri));
    }

    public async Task SetChannel(string channelId)
    {
        var requestMessage = new RequestMessage(Commands.SetChannel.Uri, new { channelId });
        await _connection.SendCommandAsync(requestMessage);
    }
}
