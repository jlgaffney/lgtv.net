using LgTv.Clients.Apps;
using LgTv.Clients.Audio;
using LgTv.Clients.Channels;
using LgTv.Clients.Display;
using LgTv.Clients.Info;
using LgTv.Clients.Inputs;
using LgTv.Clients.Mouse;
using LgTv.Clients.Notifications;
using LgTv.Clients.Playback;
using LgTv.Clients.Power;

namespace LgTv.Clients;

public enum ControlButtons
{
    Up, Down, Left, Right,
    OK,
    Back,
    Exit
}

public interface ILgTvClient : IDisposable
{
    LgTvClientConfiguration Configuration { get; }

    Task<bool> Connect();

    Task MakeHandShake();


    Task<ILgWebOsMouseClient> GetMouse();


    ILgTvInfoClient Info { get; }

    ILgTvPowerClient Power { get; }

    ILgTvAudioClient Audio { get; }

    ILgTvDisplayClient Display { get; }

    ILgTvPlaybackClient Playback { get; }

    ILgTvKeyboardClient Keyboard { get; }

    ILgTvChannelClient Channels { get; }

    ILgTvAppClient Apps { get; }

    ILgTvInputClient Inputs { get; }

    ILgTvNotificationClient Notifications { get; }


    Task<dynamic> SendCommand(RequestMessage request);
}
