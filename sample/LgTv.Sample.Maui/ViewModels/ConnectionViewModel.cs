using LgTv.Networking;
using LgTv.Scanning;
using Device = LgTv.Scanning.Device;

namespace LgTv.Sample.Maui.ViewModels;

public partial class ConnectionViewModel(
    INavigationService navigationService,
    ITvScanner scanner,
    IMacAddressResolver macAddressResolver,
    IWakeOnLan wakeOnLan,
    IClientController controller)
    : ViewModelBase(navigationService)
{
    private readonly ITvScanner _scanner = scanner ?? throw new ArgumentNullException(nameof(scanner));
    private readonly IMacAddressResolver _macAddressResolver = macAddressResolver ?? throw new ArgumentNullException(nameof(macAddressResolver));
    private readonly IWakeOnLan _wakeOnLan = wakeOnLan ?? throw new ArgumentNullException(nameof(wakeOnLan));

    [ObservableProperty]
    private string _host;

    [ObservableProperty]
    private int _port = Client.DefaultPort;

    [ObservableProperty]
    private IReadOnlyList<Device> _availableDevices;

    private Device _selectedDevice;

    public IClientController Controller { get; } = controller ?? throw new ArgumentNullException(nameof(controller));

    public Device SelectedDevice
    {
        get => _selectedDevice;
        set
        {
            if (!SetProperty(ref _selectedDevice, value))
            {
                return;
            }

            if (SelectedDevice != null)
            {
                Host = SelectedDevice.IpAddress;
            }
        }
    }

    [RelayCommand]
    public override async Task<bool> LoadAsync(CancellationToken cancellationToken = default)
    {
        OnPropertyChanged(nameof(Controller));

        if (!await base.LoadAsync(cancellationToken))
        {
            return false;
        }

        AvailableDevices = null;

        if (!Controller.IsConnected)
        {
            var devices = await _scanner.GetDevices();

            AvailableDevices = devices
                .OrderBy(x => x.Name, StringComparer.Ordinal)
                .ToArray();
        }

        return true;
    }

    [RelayCommand]
    private async Task Connect()
    {
        if (Controller.IsConnected)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(Host) || Port <= 0)
        {
            // TODO Show error
            return;
        }

        if (!Host.IsIPAddress())
        {
            // TODO Show error
            return;
        }

        if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            await WakeDevice(Host);
        }

        var tvHostConfiguration = new HostConfiguration
        {
            Host = Host,
            Port = Port
        };

        await Controller.Connect(tvHostConfiguration);
        OnPropertyChanged(nameof(Controller));
    }

    [RelayCommand]
    private async Task Disconnect()
    {
        await Controller.Disconnect();

        await LoadAsync();
    }

    private async Task WakeDevice(string ipAddress)
    {
        var macAddress = await _macAddressResolver.ResolveAsync(ipAddress);

        await _wakeOnLan.SendMagicPacketAsync(macAddress);
    }
}
