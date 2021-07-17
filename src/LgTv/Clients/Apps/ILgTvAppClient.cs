using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LgTv.Clients.Apps
{
    public interface ILgTvAppClient
    {
        Task<IEnumerable<App>> GetApps();

        Task<string> LaunchApp(string id, Uri uri = null);

        Task<string> LaunchYouTube(string videoId);

        Task<string> LaunchYouTube(Uri uri);

        Task<string> LaunchWebBrowser(Uri uri);

        Task<string> CloseApp(string id);
    }
}
