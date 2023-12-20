namespace LgTv.Sample.Maui;

[ObservableObject]
public partial class AppShell : IDisposable
{
    private readonly IClientController _controller;

    public AppShell(IClientController controller)
    {
        InitializeComponent();

        RegisterRoutes();

        _controller = controller;

        _controller.Connected += ControllerOnConnected;
        _controller.Disconnected += ControllerOnDisconnected;

        UpdateFlyoutItems();
    }

    public void Dispose()
    {
        _controller.Connected -= ControllerOnConnected;
        _controller.Disconnected -= ControllerOnDisconnected;
    }

    private async void ControllerOnConnected(object sender, EventArgs e)
    {
        UpdateFlyoutItems();
        await GoToAsync("//home");
    }

    private async void ControllerOnDisconnected(object sender, EventArgs e)
    {
        UpdateFlyoutItems();
        await GoToAsync("//connect");
    }

    private static void RegisterRoutes()
    {
        Routing.RegisterRoute("apps/app", typeof(AppView));
        Routing.RegisterRoute("channels/channel", typeof(ChannelView));
        Routing.RegisterRoute("inputs/input", typeof(Views.InputView));
    }

    private void UpdateFlyoutItems()
    {
        ConnectItem.FlyoutItemIsVisible = !_controller.IsConnected;

        HomeItem.FlyoutItemIsVisible = _controller.IsConnected;
        PowerItem.FlyoutItemIsVisible = _controller.IsConnected;
        AudioItem.FlyoutItemIsVisible = _controller.IsConnected;
        PlaybackItem.FlyoutItemIsVisible = _controller.IsConnected;
        KeyboardItem.FlyoutItemIsVisible = _controller.IsConnected;
        DpadItem.FlyoutItemIsVisible = _controller.IsConnected;
        AppsItem.FlyoutItemIsVisible = _controller.IsConnected;
        ChannelsItem.FlyoutItemIsVisible = _controller.IsConnected;
        InputsItem.FlyoutItemIsVisible = _controller.IsConnected;
        NotificationsItem.FlyoutItemIsVisible = _controller.IsConnected;
        ServicesItem.FlyoutItemIsVisible = _controller.IsConnected;
        InfoItem.FlyoutItemIsVisible = _controller.IsConnected;
        DeveloperItem.FlyoutItemIsVisible = _controller.IsConnected;
        DisconnectItem.FlyoutItemIsVisible = _controller.IsConnected;
    }
}
