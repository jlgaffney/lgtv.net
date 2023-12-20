namespace LgTv.Sample.Maui.ViewModels;

public partial class PowerViewModel(
        INavigationService navigationService,
        IClientController controller)
    : ConnectedViewModelBase(navigationService, controller)
{
    [RelayCommand]
    private async Task TurnOff()
    {
        await Controller.Client.Power.TurnOff();
    }
}
