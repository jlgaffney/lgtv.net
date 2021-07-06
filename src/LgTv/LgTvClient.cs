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

            Power = new LgTvPowerClient(_connection);
            Volume = new LgTvVolumeClient(_connection);
            Channels = new LgTvChannelClient(_connection);
            Playback = new LgTvPlaybackClient(_connection);
            Apps = new LgTvAppClient(_connection);
            Inputs = new LgTvInputClient(_connection);
            Display = new LgTvDisplayClient(_connection);
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
        

        public async Task<ILgWebOsMouseClient> GetMouse()
        {
            var requestMessage = new RequestMessage("ssap://com.webos.service.networkinput/getPointerInputSocket", new { });
            var response = await _connection.SendCommandAsync(requestMessage);
            var socketPath = (string) response.socketPath;
           
            var mouseConnection = new LgWebOsMouseClient(new LgTvConnection());
            
            await mouseConnection.Connect(socketPath);
            
            return mouseConnection;
        }


        public async Task ShowToast()
        {
            var requestMessage = new RequestMessage("ssap://system.notifications/createToast", new { message = "Co tam u Ciebie?" });
            await _connection.SendCommandAsync(requestMessage);
        }


        public ILgTvPowerClient Power { get; }

        public ILgTvVolumeClient Volume { get; }
        
        public ILgTvChannelClient Channels { get; }
        
        public ILgTvPlaybackClient Playback { get; }
        
        public ILgTvAppClient Apps { get; }
        
        public ILgTvInputClient Inputs { get; }
        
        public ILgTvDisplayClient Display { get; }



        public void Dispose()
        {
           _connection?.Dispose();
        }
    }
}