namespace LgTv.Sample.Maui.ViewModels;

[QueryProperty(nameof(Id), "id")]
public partial class AppViewModel(
        INavigationService navigationService,
        IClientController controller)
    : ItemViewModelBase<Features.Apps.App, string>(navigationService, controller)
{
    protected override Task<Features.Apps.App> GetItem(string id, CancellationToken cancellationToken = default)
    {
        return Controller.Client.Apps.GetApp(Id);
    }

    [RelayCommand]
    private async Task Launch()
    {
        await Controller.Client.Apps.LaunchApp(Id);
    }
}
