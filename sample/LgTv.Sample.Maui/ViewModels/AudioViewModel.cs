namespace LgTv.Sample.Maui.ViewModels;

public partial class AudioViewModel(
        INavigationService navigationService,
        IClientController controller)
    : ConnectedViewModelBase(navigationService, controller)
{
    [ObservableProperty]
    private string _soundOutput;

    [ObservableProperty]
    private int? _volume;

    [ObservableProperty]
    private bool _isMuted;

    public override async Task<bool> LoadAsync(CancellationToken cancellationToken = default)
    {
        if (!await base.LoadAsync(cancellationToken))
        {
            return false;
        }

        SoundOutput = null;
        Volume = null;
        IsMuted = false;

        SoundOutput = await Controller.Client.Audio.GetOutput();
        Volume = await Controller.Client.Audio.GetVolume();
        IsMuted = await Controller.Client.Audio.IsMuted();

        return true;
    }

    [RelayCommand]
    private async Task SetVolume()
    {
        if (!Volume.HasValue)
        {
            return;
        }

        await Controller.Client.Audio.SetVolume(Volume.Value);
    }

    [RelayCommand]
    private async Task ToggleMute()
    {
        await Controller.Client.Audio.SetMute(!IsMuted);

        IsMuted = !IsMuted;
    }

    [RelayCommand]
    private async Task VolumeUp()
    {
        await Controller.Client.Audio.VolumeUp();
        Volume = await Controller.Client.Audio.GetVolume();
    }

    [RelayCommand]
    private async Task VolumeDown()
    {
        await Controller.Client.Audio.VolumeDown();
        Volume = await Controller.Client.Audio.GetVolume();
    }
}
