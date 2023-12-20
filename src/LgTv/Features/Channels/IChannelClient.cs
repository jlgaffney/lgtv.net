namespace LgTv.Features.Channels;

public interface IChannelClient
{
    Task<Channel> GetChannel(string id);

    Task<IEnumerable<Channel>> GetChannels();

    Task<Channel> GetCurrentChannel();

    Task ChannelUp();

    Task ChannelDown();

    Task SetChannel(string id);
}
