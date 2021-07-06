using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LgTv
{
    public enum ControlButtons
    {
        Back,Down,Left,Right,
        OK,
        Exit
    }

    public class LgTvClient : ILgTvClient
    {
        private readonly ILgTvConnection _connection;
        private readonly IClientKeyStore _keyStore;

        private readonly Uri _webSocketUri;

        private const string BEFORE_PAIR_HAND_SHAKE = "{\"type\":\"register\",\"id\":\"register_0\",\"payload\":{\"forcePairing\":false,\"pairingType\":\"PROMPT\",\"manifest\":{\"manifestVersion\":1,\"appVersion\":\"1.1\",\"signed\":{\"created\":\"20140509\",\"appId\":\"com.lge.test\",\"vendorId\":\"com.lge\",\"localizedAppNames\":{\"\":\"LG Remote App\",\"ko-KR\":\"리모컨 앱\",\"zxx-XX\":\"ЛГ Rэмotэ AПП\"},\"localizedVendorNames\":{\"\":\"LG Electronics\"},\"permissions\":[\"TEST_SECURE\",\"CONTROL_INPUT_TEXT\",\"CONTROL_MOUSE_AND_KEYBOARD\",\"READ_INSTALLED_APPS\",\"READ_LGE_SDX\",\"READ_NOTIFICATIONS\",\"SEARCH\",\"WRITE_SETTINGS\",\"WRITE_NOTIFICATION_ALERT\",\"CONTROL_POWER\",\"READ_CURRENT_CHANNEL\",\"READ_RUNNING_APPS\",\"READ_UPDATE_INFO\",\"UPDATE_FROM_REMOTE_APP\",\"READ_LGE_TV_INPUT_EVENTS\",\"READ_TV_CURRENT_TIME\"],\"serial\":\"2f930e2d2cfe083771f68e4fe7bb07\"},\"permissions\":[\"LAUNCH\",\"LAUNCH_WEBAPP\",\"APP_TO_APP\",\"CLOSE\",\"TEST_OPEN\",\"TEST_PROTECTED\",\"CONTROL_AUDIO\",\"CONTROL_DISPLAY\",\"CONTROL_INPUT_JOYSTICK\",\"CONTROL_INPUT_MEDIA_RECORDING\",\"CONTROL_INPUT_MEDIA_PLAYBACK\",\"CONTROL_INPUT_TV\",\"CONTROL_POWER\",\"READ_APP_STATUS\",\"READ_CURRENT_CHANNEL\",\"READ_INPUT_DEVICE_LIST\",\"READ_NETWORK_STATE\",\"READ_RUNNING_APPS\",\"READ_TV_CHANNEL_LIST\",\"WRITE_NOTIFICATION_TOAST\",\"READ_POWER_STATE\",\"READ_COUNTRY_INFO\"],\"signatures\":[{\"signatureVersion\":1,\"signature\":\"eyJhbGdvcml0aG0iOiJSU0EtU0hBMjU2Iiwia2V5SWQiOiJ0ZXN0LXNpZ25pbmctY2VydCIsInNpZ25hdHVyZVZlcnNpb24iOjF9.hrVRgjCwXVvE2OOSpDZ58hR+59aFNwYDyjQgKk3auukd7pcegmE2CzPCa0bJ0ZsRAcKkCTJrWo5iDzNhMBWRyaMOv5zWSrthlf7G128qvIlpMT0YNY+n/FaOHE73uLrS/g7swl3/qH/BGFG2Hu4RlL48eb3lLKqTt2xKHdCs6Cd4RMfJPYnzgvI4BNrFUKsjkcu+WD4OO2A27Pq1n50cMchmcaXadJhGrOqH5YmHdOCj5NSHzJYrsW0HPlpuAx/ECMeIZYDh6RMqaFM2DXzdKX9NmmyqzJ3o/0lkk/N97gfVRLW5hA29yeAwaCViZNCP8iC9aO0q9fQojoa7NQnAtw==\"}]}}}";
        
        private const string AFTER_PAIR_HAND_SHAKE = "{\"type\":\"register\",\"id\":\"register_0\",\"payload\":{\"forcePairing\":false,\"pairingType\":\"PROMPT\",\"client-key\":\"CLIENTKEYGOESHERE\",\"manifest\":{\"manifestVersion\":1,\"appVersion\":\"1.1\",\"signed\":{\"created\":\"20140509\",\"appId\":\"com.lge.test\",\"vendorId\":\"com.lge\",\"localizedAppNames\":{\"\":\"LG Remote App\",\"ko-KR\":\"리모컨 앱\",\"zxx-XX\":\"ЛГ Rэмotэ AПП\"},\"localizedVendorNames\":{\"\":\"LG Electronics\"},\"permissions\":[\"TEST_SECURE\",\"CONTROL_INPUT_TEXT\",\"CONTROL_MOUSE_AND_KEYBOARD\",\"READ_INSTALLED_APPS\",\"READ_LGE_SDX\",\"READ_NOTIFICATIONS\",\"SEARCH\",\"WRITE_SETTINGS\",\"WRITE_NOTIFICATION_ALERT\",\"CONTROL_POWER\",\"READ_CURRENT_CHANNEL\",\"READ_RUNNING_APPS\",\"READ_UPDATE_INFO\",\"UPDATE_FROM_REMOTE_APP\",\"READ_LGE_TV_INPUT_EVENTS\",\"READ_TV_CURRENT_TIME\"],\"serial\":\"2f930e2d2cfe083771f68e4fe7bb07\"},\"permissions\":[\"LAUNCH\",\"LAUNCH_WEBAPP\",\"APP_TO_APP\",\"CLOSE\",\"TEST_OPEN\",\"TEST_PROTECTED\",\"CONTROL_AUDIO\",\"CONTROL_DISPLAY\",\"CONTROL_INPUT_JOYSTICK\",\"CONTROL_INPUT_MEDIA_RECORDING\",\"CONTROL_INPUT_MEDIA_PLAYBACK\",\"CONTROL_INPUT_TV\",\"CONTROL_POWER\",\"READ_APP_STATUS\",\"READ_CURRENT_CHANNEL\",\"READ_INPUT_DEVICE_LIST\",\"READ_NETWORK_STATE\",\"READ_RUNNING_APPS\",\"READ_TV_CHANNEL_LIST\",\"WRITE_NOTIFICATION_TOAST\",\"READ_POWER_STATE\",\"READ_COUNTRY_INFO\"],\"signatures\":[{\"signatureVersion\":1,\"signature\":\"eyJhbGdvcml0aG0iOiJSU0EtU0hBMjU2Iiwia2V5SWQiOiJ0ZXN0LXNpZ25pbmctY2VydCIsInNpZ25hdHVyZVZlcnNpb24iOjF9.hrVRgjCwXVvE2OOSpDZ58hR+59aFNwYDyjQgKk3auukd7pcegmE2CzPCa0bJ0ZsRAcKkCTJrWo5iDzNhMBWRyaMOv5zWSrthlf7G128qvIlpMT0YNY+n/FaOHE73uLrS/g7swl3/qH/BGFG2Hu4RlL48eb3lLKqTt2xKHdCs6Cd4RMfJPYnzgvI4BNrFUKsjkcu+WD4OO2A27Pq1n50cMchmcaXadJhGrOqH5YmHdOCj5NSHzJYrsW0HPlpuAx/ECMeIZYDh6RMqaFM2DXzdKX9NmmyqzJ3o/0lkk/N97gfVRLW5hA29yeAwaCViZNCP8iC9aO0q9fQojoa7NQnAtw==\"}]}}}";

        private string _currentPariKey;

        public LgTvClient(
            ILgTvConnection connection,
            IClientKeyStore keyStore,
            string hostname,
            int port = 3000)
        {
            _connection = connection;
            _keyStore = keyStore;
            
            _webSocketUri = new Uri(FormattableString.Invariant($"ws://{hostname}:{port}"));
        }

        
        public async Task<bool> Connect()
        {
            return await _connection.Connect(_webSocketUri);
        }

        public async Task MakeHandShake()
        {
            _currentPariKey = await _keyStore.GetClientKey();
            dynamic result;
            if (_currentPariKey != null)
            {
                var key = AFTER_PAIR_HAND_SHAKE.Replace("CLIENTKEYGOESHERE", _currentPariKey);//"e09a9d18150bf244ff43c1b2594c4ed6");
                result = await _connection.SendCommandAsync(key);
                await _keyStore.SetClientKey((string) result.clientKey);
                return;
            }

            result = await _connection.SendCommandAsync(BEFORE_PAIR_HAND_SHAKE);
            await _keyStore.SetClientKey(result.clientKey);
        }


        public async Task TurnOff()
        {
            await _connection.SendCommandAsync(new RequestMessage("", "ssap://system/turnOff"));
        }


        public async Task<ILgWebOsMouseService> GetMouse()
        {
            var requestMessage = new RequestMessage("ssap://com.webos.service.networkinput/getPointerInputSocket", new { });
            var result = await _connection.SendCommandAsync(requestMessage);
            var socketPath = (string) result.socketPath;
           
            var mouseConnection = new LgWebOsMouseService(new LgTvConnection());
            
            await mouseConnection.Connect(socketPath);
            
            return mouseConnection;
        }


        public async Task ShowToast()
        {
            var requestMessage = new RequestMessage("ssap://system.notifications/createToast", new { message = "Co tam u Ciebie?" });
            await _connection.SendCommandAsync(requestMessage);
        }


        public async Task<int> GetVolume()
        {
            // {
            //     "type": "response",
            //     "id": "status_1",
            //     "payload": {
            //         "muted": false,
            //         "scenario": "mastervolume_tv_speaker",
            //         "active": false,
            //         "action": "requested",
            //         "volume": 7,
            //         "returnValue": true,
            //         "subscribed": true
            //     }
            // }
            var requestMessage = new RequestMessage("status", "ssap://audio/getVolume");
            var response = await _connection.SendCommandAsync(requestMessage);
            return (bool) response.muted ? -1 : (int) response.volume;
        }

        public async Task<bool> IsMuted()
        {
            var requestMessage = new RequestMessage("status", "ssap://audio/getStatus");
            var response = await _connection.SendCommandAsync(requestMessage);
            return (bool) response.mute;
        }

        public async Task VolumeUp()
        {
            var requestMessage = new RequestMessage("volumeup", "ssap://audio/volumeUp");
            await _connection.SendCommandAsync(requestMessage);
        }

        public async Task VolumeDown()
        {
            var requestMessage = new RequestMessage("volumedown", "ssap://audio/volumeDown");
            await _connection.SendCommandAsync(requestMessage);
        }

        public async Task SetVolume(int value)
        {
            if (value < 0 || value > 100)
            {
                return;
            }

            var requestMessage = new RequestMessage("ssap://audio/setVolume", new { volume = value });
            await _connection.SendCommandAsync(requestMessage);
        }

        public async Task SetMute(bool value)
        {
            var requestMessage = new RequestMessage("ssap://audio/setMute", new { mute = value });
            await _connection.SendCommandAsync(requestMessage);
        }

        public async Task ToggleMute()
        {
            await SetMute(!await IsMuted());
        }


        public async Task<IEnumerable<Channel>> GetChannels()
        {
            var requestMessage = new RequestMessage("channels", "ssap://tv/getChannelList");
            var result = await _connection.SendCommandAsync(requestMessage);
            
            var channels = new List<Channel>();
            foreach (var c in result.channelList)
            {
                channels.Add(new Channel
                {
                    Id = c.channelId,
                    Name = c.channelName,
                    Number = int.Parse((string) c.channelNumber)
                });
            }

            return channels.OrderBy(x => x.Number);
        }

        public async Task<Channel> GetCurrentChannel()
        {
            var requestMessage = new RequestMessage("channels", "ssap://tv/getCurrentChannel");
            var result = await _connection.SendCommandAsync(requestMessage);

            return new Channel
            {
                Id = result.channelId,
                Name = result.channelName,
                Number = int.Parse(result.channelNumber)
            };
        }
        
        public async Task GetChannelProgramInfo()
        {
            var requestMessage = new RequestMessage("programinfo", "ssap://tv/getChannelProgramInfo");
            await _connection.SendCommandAsync(requestMessage);
        }

        public async Task ChannelUp()
        {
            await _connection.SendCommandAsync(new RequestMessage("channelUp", "ssap://tv/channelUp"));
        }

        public async Task ChannelDown()
        {
            await _connection.SendCommandAsync(new RequestMessage("channelDown", "ssap://tv/channelDown"));
        }

        public async Task SetChannel(string channelId)
        {
            var requestMessage = new RequestMessage("ssap://tv/openChannel", new { channelId });
            await _connection.SendCommandAsync(requestMessage);
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


        public async Task<IEnumerable<App>> GetApps()
        {
            var requestMessage = new RequestMessage("launcher", "ssap://com.webos.applicationManager/listLaunchPoints");
            var results = await _connection.SendCommandAsync(requestMessage);

            var apps = new List<App>();
            foreach (var c in results.launchPoints)
            {
                apps.Add(new App
                {
                    Id = c.id,
                    LaunchPointId = c.launchPointId,
                    Title = c.title,
                    Icon = c.icon
                });
            }

            return apps.OrderBy(x => x.Title);
        }

        public async Task<string> LaunchApp(string appId, Uri uri = null)
        {
            dynamic requestPayload = new { id = appId };
            if (uri != null)
            {
                requestPayload.@params = new { contentTarget = uri.ToString() };
            }

            var requestMessage = new RequestMessage("ssap://system.launcher/launch", requestPayload);
            var response = await _connection.SendCommandAsync(requestMessage);
            return (string) response.sessionId;
        }

        public async Task<string> LaunchYouTube(string videoId)
        {
            return await LaunchYouTube(new Uri($"http://www.youtube.com/tv?v={videoId}"));
        }

        public async Task<string> LaunchYouTube(Uri uri)
        {
            return await LaunchApp("youtube.leanback.v4", uri);
        }

        public async Task<string> LaunchWebBrowser(Uri uri)
        {
            var requestMessage = new RequestMessage("ssap://system.launcher/open", new { target = uri.ToString() });
            var response = await _connection.SendCommandAsync(requestMessage);
            return (string) response.sessionId;
        }

        public async Task<string> CloseApp(string appId)
        {
            var requestMessage = new RequestMessage("ssap://system.launcher/close", new { id = appId });
            var response = await _connection.SendCommandAsync(requestMessage);
            return (string) response.sessionId;
        }


        public async Task<IEnumerable<ExternalInput>> GetInputs()
        {
            var requestMessage = new RequestMessage("input", "ssap://tv/getExternalInputList");
            var results = await _connection.SendCommandAsync(requestMessage);

            var inputs = new List<ExternalInput>();
            foreach (var result in results)
            {
                inputs.Add(new ExternalInput(result.id, result.label)
                {
                    Icon = result.icon
                });
            }

            return inputs.OrderBy(x => x.Label);
        }

        public async Task SetInput(string id)
        {
            var requestMessage = new RequestMessage("ssap://tv/switchInput", new { inputId = id });
            await _connection.SendCommandAsync(requestMessage);
        }


        public async Task TurnOn3D()
        {
            await _connection.SendCommandAsync(new RequestMessage("3d", "ssap://com.webos.service.tv.display/set3DOn"));
        }

        public async Task TurnOff3D()
        {
            await _connection.SendCommandAsync(new RequestMessage("3d", "ssap://com.webos.service.tv.display/set3DOff"));
        }

        public async Task<bool> IsTurnedOn3D()
        {
            //Response: { returnValue: true,  status3D: { status: true, pattern: ’2Dto3D’ } }
            var requestMessage = new RequestMessage("status3D", "ssap://com.webos.service.tv.display/get3DStatus");
            var response = await _connection.SendCommandAsync(requestMessage);
            return (bool) response.status3D.status;
        }



        public void Dispose()
        {
           _connection?.Dispose();
        }
    }
}