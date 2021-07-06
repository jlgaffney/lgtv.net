using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LgTv
{
    public interface ILgTvClient : IDisposable
    {
        Task<bool> Connect();

        Task MakeHandShake();
        
        Task TurnOn();

        Task TurnOff();


        Task ControlButton(object ok);

        Task<LgWebOsMouseService> GetMouse();

        Task<IEnumerable<Channel>> ChannelList();

        Task<Channel> GetChannel();

        Task ShowToast();


        Task<string> LaunchApp(string appId, Uri uri);



        Task VolumeDown();

        Task VolumeUp();

        Task ChannelInfo();

        Task SetChannel(string channelId);

        Task SetMute(bool value);

        Task ToggleMute();

        Task<bool> IsMuted();

        Task SetVolume(int value);

        Task Play();

        Task Pause();

        Task ChannelUp();

        Task ChannelDown();

        Task Stop();

        Task TurnOn3D();

        Task TurnOff3D();

        Task<bool> IsTurnedOn3D();

        Task<int> GetVolume();

        Task<IEnumerable<ExternalInput>> GetInputList();

        Task SetInput(string id);
        
        Task<IEnumerable<App>> GetApps();

        Task<string> LaunchApp(string appId);

        Task<string> CloseApp(string appId);

        Task<string> OpenWebBrowser(Uri uri);

        Task<string> LaunchYouTube(string videoId);

        Task<string> LaunchYouTube(Uri uri);

        Task CloseAsync();
    }
}
