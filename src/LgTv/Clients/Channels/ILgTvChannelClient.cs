namespace LgTv.Clients.Channels;

public interface ILgTvChannelClient
{
    Task<Channel> GetChannel(string id);

    Task<IEnumerable<Channel>> GetChannels();

    Task<Channel> GetCurrentChannel();

    Task ChannelUp();

    Task ChannelDown();

    Task SetChannel(string id);
}
