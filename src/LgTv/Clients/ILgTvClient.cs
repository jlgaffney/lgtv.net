using System;
using System.Threading.Tasks;
using LgTv.Apps;
using LgTv.Audio;
using LgTv.Channels;
using LgTv.Display;
using LgTv.Info;
using LgTv.Inputs;
using LgTv.Mouse;
using LgTv.Notifications;
using LgTv.Playback;
using LgTv.Power;

namespace LgTv.Clients
{
    public enum ControlButtons
    {
        Up, Down, Left, Right,
        OK,
        Back,
        Exit
    }

    public interface ILgTvClient : IDisposable
    {
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
}
