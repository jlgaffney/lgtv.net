using LgTv.Extensions;
using LgTv.Features.Apps;
using LgTv.Features.Audio;
using LgTv.Features.Channels;
using LgTv.Features.Info;
using LgTv.Features.Inputs;
using LgTv.Features.Notifications;
using LgTv.Features.Playback;
using LgTv.Features.Power;
using LgTv.Interaction;
using LgTv.Networking;
using LgTv.Stores;

namespace LgTv;

public class Client : IClient
{
    public const int DefaultPort = 3001;

    private const string WebSocketScheme = "wss";

    private const string ClientKeyPlaceholder = "CLIENTKEYGOESHERE";
    private const string BeforePairHandShake = """{"type":"register","id":"register_0","payload":{"forcePairing":false,"pairingType":"PROMPT","manifest":{"manifestVersion":1,"appVersion":"1.1","signed":{"created":"20140509","appId":"com.lge.test","vendorId":"com.lge","localizedAppNames":{"":"LG Remote App","ko-KR":"리모컨 앱","zxx-XX":"ЛГ Rэмotэ AПП"},"localizedVendorNames":{"":"LG Electronics"},"permissions":["TEST_SECURE","CONTROL_INPUT_TEXT","CONTROL_MOUSE_AND_KEYBOARD","READ_INSTALLED_APPS","READ_LGE_SDX","READ_NOTIFICATIONS","SEARCH","WRITE_SETTINGS","WRITE_NOTIFICATION_ALERT","CONTROL_POWER","READ_CURRENT_CHANNEL","READ_RUNNING_APPS","READ_UPDATE_INFO","UPDATE_FROM_REMOTE_APP","READ_LGE_TV_INPUT_EVENTS","READ_TV_CURRENT_TIME"],"serial":"2f930e2d2cfe083771f68e4fe7bb07"},"permissions":["LAUNCH","LAUNCH_WEBAPP","APP_TO_APP","CLOSE","TEST_OPEN","TEST_PROTECTED","CONTROL_AUDIO","CONTROL_DISPLAY","CONTROL_INPUT_JOYSTICK","CONTROL_INPUT_MEDIA_RECORDING","CONTROL_INPUT_MEDIA_PLAYBACK","CONTROL_INPUT_TV","CONTROL_POWER","READ_APP_STATUS","READ_CURRENT_CHANNEL","READ_INPUT_DEVICE_LIST","READ_NETWORK_STATE","READ_RUNNING_APPS","READ_TV_CHANNEL_LIST","WRITE_NOTIFICATION_TOAST","READ_POWER_STATE","READ_COUNTRY_INFO"],"signatures":[{"signatureVersion":1,"signature":"eyJhbGdvcml0aG0iOiJSU0EtU0hBMjU2Iiwia2V5SWQiOiJ0ZXN0LXNpZ25pbmctY2VydCIsInNpZ25hdHVyZVZlcnNpb24iOjF9.hrVRgjCwXVvE2OOSpDZ58hR+59aFNwYDyjQgKk3auukd7pcegmE2CzPCa0bJ0ZsRAcKkCTJrWo5iDzNhMBWRyaMOv5zWSrthlf7G128qvIlpMT0YNY+n/FaOHE73uLrS/g7swl3/qH/BGFG2Hu4RlL48eb3lLKqTt2xKHdCs6Cd4RMfJPYnzgvI4BNrFUKsjkcu+WD4OO2A27Pq1n50cMchmcaXadJhGrOqH5YmHdOCj5NSHzJYrsW0HPlpuAx/ECMeIZYDh6RMqaFM2DXzdKX9NmmyqzJ3o/0lkk/N97gfVRLW5hA29yeAwaCViZNCP8iC9aO0q9fQojoa7NQnAtw=="}]}}}""";
    private const string AfterPairHandShake = """{"type":"register","id":"register_0","payload":{"forcePairing":false,"pairingType":"PROMPT","client-key":""" + $"\"{ClientKeyPlaceholder}\"" + ""","manifest":{"manifestVersion":1,"appVersion":"1.1","signed":{"created":"20140509","appId":"com.lge.test","vendorId":"com.lge","localizedAppNames":{"":"LG Remote App","ko-KR":"리모컨 앱","zxx-XX":"ЛГ Rэмotэ AПП"},"localizedVendorNames":{"":"LG Electronics"},"permissions":["TEST_SECURE","CONTROL_INPUT_TEXT","CONTROL_MOUSE_AND_KEYBOARD","READ_INSTALLED_APPS","READ_LGE_SDX","READ_NOTIFICATIONS","SEARCH","WRITE_SETTINGS","WRITE_NOTIFICATION_ALERT","CONTROL_POWER","READ_CURRENT_CHANNEL","READ_RUNNING_APPS","READ_UPDATE_INFO","UPDATE_FROM_REMOTE_APP","READ_LGE_TV_INPUT_EVENTS","READ_TV_CURRENT_TIME"],"serial":"2f930e2d2cfe083771f68e4fe7bb07"},"permissions":["LAUNCH","LAUNCH_WEBAPP","APP_TO_APP","CLOSE","TEST_OPEN","TEST_PROTECTED","CONTROL_AUDIO","CONTROL_DISPLAY","CONTROL_INPUT_JOYSTICK","CONTROL_INPUT_MEDIA_RECORDING","CONTROL_INPUT_MEDIA_PLAYBACK","CONTROL_INPUT_TV","CONTROL_POWER","READ_APP_STATUS","READ_CURRENT_CHANNEL","READ_INPUT_DEVICE_LIST","READ_NETWORK_STATE","READ_RUNNING_APPS","READ_TV_CHANNEL_LIST","WRITE_NOTIFICATION_TOAST","READ_POWER_STATE","READ_COUNTRY_INFO"],"signatures":[{"signatureVersion":1,"signature":"eyJhbGdvcml0aG0iOiJSU0EtU0hBMjU2Iiwia2V5SWQiOiJ0ZXN0LXNpZ25pbmctY2VydCIsInNpZ25hdHVyZVZlcnNpb24iOjF9.hrVRgjCwXVvE2OOSpDZ58hR+59aFNwYDyjQgKk3auukd7pcegmE2CzPCa0bJ0ZsRAcKkCTJrWo5iDzNhMBWRyaMOv5zWSrthlf7G128qvIlpMT0YNY+n/FaOHE73uLrS/g7swl3/qH/BGFG2Hu4RlL48eb3lLKqTt2xKHdCs6Cd4RMfJPYnzgvI4BNrFUKsjkcu+WD4OO2A27Pq1n50cMchmcaXadJhGrOqH5YmHdOCj5NSHzJYrsW0HPlpuAx/ECMeIZYDh6RMqaFM2DXzdKX9NmmyqzJ3o/0lkk/N97gfVRLW5hA29yeAwaCViZNCP8iC9aO0q9fQojoa7NQnAtw=="}]}}}""";

    private readonly IIPAddressResolver _ipAddressResolver;
    private readonly IMacAddressResolver _macAddressResolver;
    private readonly IWakeOnLan _wakeOnLan;
    private readonly Func<IConnection> _connectionFactory;
    private readonly IConnection _connection;
    private readonly IClientKeyStore _keyStore;

    private readonly string _ipAddress;

    private readonly Uri _webSocketUri;

    public Client(
        IIPAddressResolver ipAddressResolver,
        IMacAddressResolver macAddressResolver,
        IWakeOnLan wakeOnLan,
        Func<IConnection> connectionFactory,
        IClientKeyStore keyStore,
        string host,
        int port)
        : this(
              ipAddressResolver,
              macAddressResolver,
              wakeOnLan,
              connectionFactory,
              keyStore,
              new HostConfiguration
              {
                  Host = host,
                  Port = port
              })
    {
    }

    public Client(
        IIPAddressResolver ipAddressResolver,
        IMacAddressResolver macAddressResolver,
        IWakeOnLan wakeOnLan,
        Func<IConnection> connectionFactory,
        IClientKeyStore keyStore,
        HostConfiguration tvConfiguration,
        HostConfiguration proxyConfiguration = null)
    {
        _ipAddressResolver = ipAddressResolver ?? throw new ArgumentNullException(nameof(ipAddressResolver));
        _macAddressResolver = macAddressResolver ?? throw new ArgumentNullException(nameof(macAddressResolver));
        _wakeOnLan = wakeOnLan ?? throw new ArgumentNullException(nameof(wakeOnLan));
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        _connection = connectionFactory();
        _keyStore = keyStore ?? throw new ArgumentNullException(nameof(keyStore));

        Tv = tvConfiguration ?? throw new ArgumentNullException(nameof(tvConfiguration));

        string url;
        if (proxyConfiguration == null)
        {
            url = ToUrl(tvConfiguration);
        }
        else
        {
            // TODO Make proxy more configurable
            url = ToUrl(proxyConfiguration) + "/" + ToUrl(tvConfiguration);
        }

        _webSocketUri = new Uri(url);

        _ipAddress = tvConfiguration.Host;
        if (!_ipAddress.IsIPAddress())
        {
            _ipAddress = _ipAddressResolver.Resolve(_ipAddress);
        }

        Keyboard = new Keyboard(_connection);
        Info = new TvInfoClient(_connection);
        Power = new PowerClient(_connection, _macAddressResolver, _wakeOnLan, _ipAddress);
        Audio = new AudioClient(_connection);
        Playback = new PlaybackClient(_connection);
        Channels = new ChannelClient(_connection);
        Apps = new AppClient(_connection);
        Inputs = new InputClient(_connection);
        Notifications = new NotificationClient(_connection);
    }

    public HostConfiguration Tv { get; }

    public HostConfiguration Proxy { get; }

    public async Task<bool> Connect()
    {
        return await _connection.Connect(_webSocketUri);
    }

    public async Task MakeHandShake()
    {
        var currentPairKey = await _keyStore.GetClientKey(_ipAddress);
        dynamic result;
        if (currentPairKey != null)
        {
            var key = AfterPairHandShake.Replace(ClientKeyPlaceholder, currentPairKey);
            result = await _connection.SendCommandAsync(key);
            await _keyStore.SetClientKey(_ipAddress, (string)result.clientKey);
            return;
        }

        result = await _connection.SendCommandAsync(BeforePairHandShake);
        await _keyStore.SetClientKey(_ipAddress, result.clientKey);
    }


    public Task<IMouse> Mouse => GetMouse();

    public IKeyboard Keyboard { get; }

    public ITvInfoClient Info { get; }

    public IPowerClient Power { get; }

    public IAudioClient Audio { get; }

    public IPlaybackClient Playback { get; }

    public IChannelClient Channels { get; }

    public IAppClient Apps { get; }

    public IInputClient Inputs { get; }

    public INotificationClient Notifications { get; }


    public async Task<dynamic> SendCommand(RequestMessage message)
    {
        return await _connection.SendCommandAsync(message);
    }


    public async ValueTask DisposeAsync()
    {
        if (_connection != null)
        {
            await _connection.DisposeAsync();
        }
    }

    private async Task<IMouse> GetMouse()
    {
        var requestMessage = new RequestMessage(Commands.GetMouse.Uri);
        var response = await _connection.SendCommandAsync(requestMessage);
        var socketPath = (string)response.socketPath;

        if (Proxy != null)
        {
            socketPath = $"{ToUrl(Proxy)}/{socketPath}";
        }

        var mouseConnection = new Mouse(_connectionFactory());

        await mouseConnection.Connect(socketPath);

        return mouseConnection;
    }

    private static string ToUrl(HostConfiguration configuration)
    {
        return new UriBuilder(WebSocketScheme, configuration.Host, configuration.Port).ToString().TrimEnd('/');
    }
}
