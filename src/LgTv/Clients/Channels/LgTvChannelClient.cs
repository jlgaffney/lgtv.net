using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LgTv.Connections;

namespace LgTv.Clients.Channels
{
    internal class LgTvChannelClient : ILgTvChannelClient
    {
        private readonly ILgTvConnection _connection;

        public LgTvChannelClient(
            ILgTvConnection connection)
        {
            _connection = connection;
        }

        public async Task<Channel> GetChannel(string id)
        {
            return (await GetChannels()).FirstOrDefault(x => string.Equals(id, x.Id, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<IEnumerable<Channel>> GetChannels()
        {
            var requestMessage = new RequestMessage(LgTvCommands.GetChannels.Prefix, LgTvCommands.GetChannels.Uri);
            var response = await _connection.SendCommandAsync(requestMessage);

            var channels = new List<Channel>();
            foreach (var channel in response.channelList)
            {
                channels.Add(new Channel
                {
                    Id = channel.channelId,
                    Name = channel.channelName,
                    Number = int.Parse((string) channel.channelNumber)
                });
            }

            return channels.OrderBy(x => x.Number);
        }

        public async Task<Channel> GetCurrentChannel()
        {
            var requestMessage = new RequestMessage(LgTvCommands.GetCurrentChannel.Prefix, LgTvCommands.GetCurrentChannel.Uri);
            var response = await _connection.SendCommandAsync(requestMessage);

            if (response.channelId == null)
            {
                return null;
            }

            return new Channel
            {
                Id = response.channelId,
                Name = response.channelName,
                Number = int.Parse((string) response.channelNumber)
            };
        }

        public async Task GetCurrentChannelProgramInfo()
        {
            var requestMessage = new RequestMessage(LgTvCommands.GetCurrentChannelProgramInfo.Prefix, LgTvCommands.GetCurrentChannelProgramInfo.Uri);
            var response = await _connection.SendCommandAsync(requestMessage);

            // TODO Get info from response
        }

        public async Task ChannelUp()
        {
            await _connection.SendCommandAsync(new RequestMessage(LgTvCommands.SetChannelUp.Prefix, LgTvCommands.SetChannelUp.Uri));
        }

        public async Task ChannelDown()
        {
            await _connection.SendCommandAsync(new RequestMessage(LgTvCommands.SetChannelDown.Prefix, LgTvCommands.SetChannelDown.Uri));
        }

        public async Task SetChannel(string channelId)
        {
            var requestMessage = new RequestMessage(LgTvCommands.SetChannel.Uri, new { channelId });
            await _connection.SendCommandAsync(requestMessage);
        }
    }
}
