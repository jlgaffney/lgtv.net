using LgTv.Connections;

namespace LgTv.Clients.Playback;

internal class LgTvPlaybackClient(ILgTvConnection connection) : ILgTvPlaybackClient
{
    public async Task Play()
    {
        await connection.SendCommandAsync(new RequestMessage(LgTvCommands.MediaPlay.Prefix, LgTvCommands.MediaPlay.Uri));
    }

    public async Task Pause()
    {
        await connection.SendCommandAsync(new RequestMessage(LgTvCommands.MediaPause.Prefix, LgTvCommands.MediaPause.Uri));
    }

    public async Task Stop()
    {
        await connection.SendCommandAsync(new RequestMessage(LgTvCommands.MediaStop.Prefix, LgTvCommands.MediaStop.Uri));
    }

    public async Task FastForward()
    {
        await connection.SendCommandAsync(new RequestMessage(LgTvCommands.MediaFastForward.Prefix, LgTvCommands.MediaFastForward.Uri));
    }

    public async Task Rewind()
    {
        await connection.SendCommandAsync(new RequestMessage(LgTvCommands.MediaRewind.Prefix, LgTvCommands.MediaRewind.Uri));
    }
}
