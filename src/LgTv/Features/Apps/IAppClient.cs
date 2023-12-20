namespace LgTv.Features.Apps;

public interface IAppClient
{
    Task<ForegroundAppInfo> GetForegroundAppInfo();

    Task<App> GetApp(string id);

    Task<IEnumerable<App>> GetApps();

    Task<string> LaunchApp(string id, Uri uri = null);

    Task<string> LaunchYouTube(string videoId);

    Task<string> LaunchYouTube(Uri uri);

    Task<string> LaunchWebBrowser(Uri uri);

    Task<string> CloseApp(string id);
}
