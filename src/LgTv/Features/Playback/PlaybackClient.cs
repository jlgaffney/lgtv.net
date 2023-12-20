namespace LgTv.Features.Playback;

internal class PlaybackClient(IConnection connection) : IPlaybackClient
{
    private readonly IConnection _connection = connection ?? throw new ArgumentNullException(nameof(connection));

    public async Task Play()
    {
        await _connection.SendCommandAsync(new RequestMessage(Commands.MediaPlay.Prefix, Commands.MediaPlay.Uri));
    }

    public async Task Pause()
    {
        await _connection.SendCommandAsync(new RequestMessage(Commands.MediaPause.Prefix, Commands.MediaPause.Uri));
    }

    public async Task Stop()
    {
        await _connection.SendCommandAsync(new RequestMessage(Commands.MediaStop.Prefix, Commands.MediaStop.Uri));
    }

    public async Task FastForward()
    {
        await _connection.SendCommandAsync(new RequestMessage(Commands.MediaFastForward.Prefix, Commands.MediaFastForward.Uri));
    }

    public async Task Rewind()
    {
        await _connection.SendCommandAsync(new RequestMessage(Commands.MediaRewind.Prefix, Commands.MediaRewind.Uri));
    }
}
