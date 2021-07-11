using System;
using System.Threading.Tasks;
using LgTv.Apps;
using LgTv.Channels;
using LgTv.Display;
using LgTv.Inputs;
using LgTv.Mouse;
using LgTv.Playback;
using LgTv.Power;
using LgTv.Volume;

namespace LgTv
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

        
        ILgTvPowerClient Power { get; }

        ILgTvVolumeClient Volume { get; }

        ILgTvChannelClient Channels { get; }

        ILgTvPlaybackClient Playback { get; }

        ILgTvAppClient Apps { get; }

        ILgTvInputClient Inputs { get; }

        ILgTvDisplayClient Display { get; }
    }
}
