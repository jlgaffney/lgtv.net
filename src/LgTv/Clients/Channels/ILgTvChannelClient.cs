using System.Collections.Generic;
using System.Threading.Tasks;

namespace LgTv.Channels
{
    public interface ILgTvChannelClient
    {
        Task<IEnumerable<Channel>> GetChannels();

        Task<Channel> GetCurrentChannel();

        Task GetCurrentChannelProgramInfo();

        Task ChannelUp();

        Task ChannelDown();

        Task SetChannel(string id);
    }
}
