namespace LgTv.Features.Playback;

public interface IPlaybackClient
{
    Task Play();

    Task Pause();

    Task Stop();

    Task FastForward();

    Task Rewind();
}
