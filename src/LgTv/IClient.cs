using LgTv.Features.Apps;
using LgTv.Features.Audio;
using LgTv.Features.Channels;
using LgTv.Features.Info;
using LgTv.Features.Inputs;
using LgTv.Features.Notifications;
using LgTv.Features.Playback;
using LgTv.Features.Power;
using LgTv.Interaction;

namespace LgTv;

public interface IClient : IAsyncDisposable
{
    HostConfiguration Tv { get; }

    HostConfiguration Proxy { get; }

    Task<bool> Connect();

    Task MakeHandShake();


    Task<IMouse> Mouse { get; }

    IKeyboard Keyboard { get; }

    ITvInfoClient Info { get; }

    IPowerClient Power { get; }

    IAudioClient Audio { get; }

    IPlaybackClient Playback { get; }

    IChannelClient Channels { get; }

    IAppClient Apps { get; }

    IInputClient Inputs { get; }

    INotificationClient Notifications { get; }


    Task<dynamic> SendCommand(RequestMessage request);
}
