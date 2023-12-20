namespace LgTv.Sample.Maui.ViewModels;

public partial class NotificationsViewModel(
        INavigationService navigationService,
        IClientController controller)
    : ConnectedViewModelBase(navigationService, controller)
{
    [ObservableProperty]
    private string _toastMessage;

    [ObservableProperty]
    private bool _showWithIcon;

    [ObservableProperty]
    private string _iconUrl;

    [RelayCommand]
    private async Task ShowToast()
    {
        if (string.IsNullOrWhiteSpace(ToastMessage))
        {
            return;
        }

        if (ShowWithIcon && !string.IsNullOrWhiteSpace(IconUrl))
        {
            var handler = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true
            };

            using var client = new HttpClient(handler);

            var iconData = await client.GetByteArrayAsync(IconUrl);

            var iconExtension = Path.GetExtension(IconUrl).TrimStart('.');

            await Controller.Client.Notifications.ShowToast(ToastMessage, iconData, iconExtension);
        }
        else
        {
            await Controller.Client.Notifications.ShowToast(ToastMessage);
        }
    }
}
