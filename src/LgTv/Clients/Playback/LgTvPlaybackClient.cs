using System.Threading.Tasks;

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
            await _connection.SendCommandAsync(new RequestMessage("play", "ssap://media.controls/play"));
        }

        public async Task Pause()
        {
            await _connection.SendCommandAsync(new RequestMessage("pause", "ssap://media.controls/pause"));
        }

        public async Task Stop()
        {
            await _connection.SendCommandAsync(new RequestMessage("stop", "ssap://media.controls/stop"));
        }

        public async Task FastForward()
        {
            await _connection.SendCommandAsync(new RequestMessage("stop", "ssap://media.controls/fastForward"));
        }

        public async Task Rewind()
        {
            await _connection.SendCommandAsync(new RequestMessage("stop", "ssap://media.controls/rewind"));
        }
    }
}
