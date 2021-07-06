using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LgTv
{
    public interface ILgTvClient : IDisposable
    {
        Task<bool> Connect();

        Task MakeHandShake();

        
        Task TurnOff();


        Task<ILgWebOsMouseService> GetMouse();


        Task ShowToast();

        
        Task<int> GetVolume();

        Task<bool> IsMuted();

        Task VolumeUp();

        Task VolumeDown();

        Task SetVolume(int value);

        Task SetMute(bool value);

        Task ToggleMute();


        Task<IEnumerable<Channel>> GetChannels();

        Task<Channel> GetCurrentChannel();

        Task GetChannelProgramInfo();

        Task ChannelUp();

        Task ChannelDown();

        Task SetChannel(string id);


        Task Play();

        Task Pause();

        Task Stop();


        Task<IEnumerable<App>> GetApps();

        Task<string> LaunchApp(string id, Uri uri = null);

        Task<string> LaunchYouTube(string videoId);

        Task<string> LaunchYouTube(Uri uri);

        Task<string> LaunchWebBrowser(Uri uri);

        Task<string> CloseApp(string id);


        Task<IEnumerable<ExternalInput>> GetInputs();

        Task SetInput(string id);


        Task TurnOn3D();

        Task TurnOff3D();

        Task<bool> IsTurnedOn3D();
    }
}
