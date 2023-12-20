namespace LgTv.Sample.Maui.ViewModels;

public partial class KeyboardViewModel(
        INavigationService navigationService,
        IClientController controller)
    : ConnectedViewModelBase(navigationService, controller)
{
    [ObservableProperty]
    private string _text;

    [RelayCommand]
    private async Task InsertText()
    {
        await Controller.Client.Keyboard.InsertText(Text);
    }

    [RelayCommand]
    private async Task DeleteCharacters()
    {
        await Controller.Client.Keyboard.DeleteCharacters();
    }

    [RelayCommand]
    private async Task SendEnterKey()
    {
        await Controller.Client.Keyboard.SendEnterKey();
    }
}
