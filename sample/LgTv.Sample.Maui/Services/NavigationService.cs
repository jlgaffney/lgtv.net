namespace LgTv.Sample.Maui.Services;

public class NavigationService : INavigationService
{
    public async Task NavigateAsync(string uri)
    {
        if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            Shell.Current.Dispatcher.Dispatch(async () =>
            {
                await NavigateAsyncImpl(uri);
            });
        }
        else
        {
            await NavigateAsyncImpl(uri);
        }
    }

    private static Task NavigateAsyncImpl(string uri)
    {
        return Shell.Current.GoToAsync($"//{uri}");
    }
}
