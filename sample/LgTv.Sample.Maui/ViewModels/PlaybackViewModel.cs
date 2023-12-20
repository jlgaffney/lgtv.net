namespace LgTv.Sample.Maui.ViewModels;

public partial class PlaybackViewModel(
        INavigationService navigationService,
        IClientController controller)
    : ConnectedViewModelBase(navigationService, controller)
{
    [RelayCommand]
    private async Task Play()
    {
        await Controller.Client.Playback.Play();
    }

    [RelayCommand]
    private async Task Pause()
    {
        await Controller.Client.Playback.Pause();
    }

    [RelayCommand]
    private async Task Stop()
    {
        await Controller.Client.Playback.Stop();
    }

    [RelayCommand]
    private async Task Rewind()
    {
        await Controller.Client.Playback.Rewind();
    }

    [RelayCommand]
    private async Task FastForward()
    {
        await Controller.Client.Playback.FastForward();
    }
}
