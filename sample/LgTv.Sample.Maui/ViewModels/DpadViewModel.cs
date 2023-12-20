using LgTv.Interaction;

namespace LgTv.Sample.Maui.ViewModels;

public partial class DpadViewModel(
        INavigationService navigationService,
        IClientController controller)
    : ConnectedViewModelBase(navigationService, controller),
        IAsyncDisposable
{
    [ObservableProperty]
    private IMouse _mouse;

    public override async Task<bool> LoadAsync(CancellationToken cancellationToken = default)
    {
        if (!await base.LoadAsync(cancellationToken))
        {
            return false;
        }

        if (Mouse != null)
        {
            await Mouse.DisposeAsync();
            Mouse = null;
        }

        Mouse = await Controller.Client.Mouse;

        return true;
    }

    public async ValueTask DisposeAsync()
    {
        if (Mouse != null)
        {
            await Mouse.DisposeAsync();
            Mouse = null;
        }
    }

    [RelayCommand]
    private async Task Ok()
    {
        await Mouse.SendButton(ButtonType.Ok);
    }

    [RelayCommand]
    private async Task Back()
    {
        await Mouse.SendButton(ButtonType.Back);
    }

    [RelayCommand]
    private async Task Up()
    {
        await Mouse.SendButton(ButtonType.Up);
    }

    [RelayCommand]
    private async Task Down()
    {
        await Mouse.SendButton(ButtonType.Down);
    }

    [RelayCommand]
    private async Task Left()
    {
        await Mouse.SendButton(ButtonType.Left);
    }

    [RelayCommand]
    private async Task Right()
    {
        await Mouse.SendButton(ButtonType.Right);
    }
}
