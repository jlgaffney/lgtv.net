using LgTv.Features.Info;

namespace LgTv.Sample.Maui.ViewModels;

public partial class InfoViewModel(
        INavigationService navigationService,
        IClientController controller)
    : ConnectedViewModelBase(navigationService, controller)
{
    [ObservableProperty]
    private ConnectionInformation _connectionInfo;

    [ObservableProperty]
    private SystemInformation _systemInfo;

    [ObservableProperty]
    private SoftwareInformation _softwareInfo;

    public override async Task<bool> LoadAsync(CancellationToken cancellationToken = default)
    {
        if (!await base.LoadAsync(cancellationToken))
        {
            return false;
        }

        ConnectionInfo = null;
        SystemInfo = null;
        SoftwareInfo = null;

        ConnectionInfo = await Controller.Client.Info.GetConnectionInfo();
        SystemInfo = await Controller.Client.Info.GetSystemInfo();
        SoftwareInfo = await Controller.Client.Info.GetSoftwareInfo();

        return true;
    }
}
