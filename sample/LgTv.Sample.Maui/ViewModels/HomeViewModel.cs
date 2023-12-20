namespace LgTv.Sample.Maui.ViewModels;

public partial class HomeViewModel(
        INavigationService navigationService,
        IClientController controller)
    : ConnectedViewModelBase(navigationService, controller)
{
    [ObservableProperty]
    private Features.Apps.App _app;

    [ObservableProperty]
    private bool _loading;

    public override async Task<bool> LoadAsync(CancellationToken cancellationToken =  default)
    {
        if (!await base.LoadAsync(cancellationToken))
        {
            return false;
        }

        App = null;
        Loading = true;

        try
        {
            var foregroundAppInfo = await Controller.Client.Apps.GetForegroundAppInfo();
            if (foregroundAppInfo == null)
            {
                return true;
            }

            App = await Controller.Client.Apps.GetApp(foregroundAppInfo.AppId);

            return true;
        }
        finally
        {
            Loading = false;
        }
    }

    [RelayCommand]
    private async Task CloseCurrentApp()
    {
        if (App == null)
        {
            return;
        }

        await Controller.Client.Apps.CloseApp(App.Id);

        await LoadAsync();
    }
}
