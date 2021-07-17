using System.Threading.Tasks;
using LgTv.Connections;

namespace LgTv.Clients.Playback
{
    internal class LgTvPlaybackClient : ILgTvPlaybackClient
    {
        private readonly ILgTvConnection _connection;

        public LgTvPlaybackClient(
            ILgTvConnection connection)
        {
            _connection = connection;
        }

        public async Task Play()
        {
            await _connection.SendCommandAsync(new RequestMessage(LgTvCommands.MediaPlay.Prefix, LgTvCommands.MediaPlay.Uri));
        }

        public async Task Pause()
        {
            await _connection.SendCommandAsync(new RequestMessage(LgTvCommands.MediaPause.Prefix, LgTvCommands.MediaPause.Uri));
        }

        public async Task Stop()
        {
            await _connection.SendCommandAsync(new RequestMessage(LgTvCommands.MediaStop.Prefix, LgTvCommands.MediaStop.Uri));
        }

        public async Task FastForward()
        {
            await _connection.SendCommandAsync(new RequestMessage(LgTvCommands.MediaFastForward.Prefix, LgTvCommands.MediaFastForward.Uri));
        }

        public async Task Rewind()
        {
            await _connection.SendCommandAsync(new RequestMessage(LgTvCommands.MediaRewind.Prefix, LgTvCommands.MediaRewind.Uri));
        }
    }
}
