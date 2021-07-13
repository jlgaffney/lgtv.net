using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LgTv.Apps
{
    internal class LgTvAppClient : ILgTvAppClient
    {
        private readonly ILgTvConnection _connection;

        public LgTvAppClient(
            ILgTvConnection connection)
        {
            _connection = connection;
        }

        public async Task<ForegroundAppInfo> GetForegroundAppInfo()
        {
            var requestMessage = new RequestMessage("ssap://com.webos.applicationManager/getForegroundAppInfo", new {});
            var response = await _connection.SendCommandAsync(requestMessage);

            return new ForegroundAppInfo
            {
                AppId = response.appId,
                WindowId = response.windowId,
                ProcessId = response.processId
            };
        }

        public async Task<IEnumerable<App>> GetApps()
        {
            var requestMessage = new RequestMessage("launcher", "ssap://com.webos.applicationManager/listLaunchPoints");
            var response = await _connection.SendCommandAsync(requestMessage);

            var apps = new List<App>();
            foreach (var app in response.launchPoints)
            {
                apps.Add(new App
                {
                    Id = app.id,
                    LaunchPointId = app.launchPointId,
                    Title = app.title,
                    Icon = app.icon,
                    LargeIcon = app.largeIcon,
                    IconColorHex = app.iconColor
                });
            }

            return apps;
        }

        public async Task<string> LaunchApp(string appId, Uri uri = null)
        {
            dynamic requestPayload = new { id = appId };
            if (uri != null)
            {
                requestPayload.@params = new { contentTarget = uri.ToString() };
            }

            var requestMessage = new RequestMessage("ssap://system.launcher/launch", requestPayload);
            var response = await _connection.SendCommandAsync(requestMessage);
            return (string) response.sessionId;
        }

        public async Task<string> LaunchYouTube(string videoId)
        {
            return await LaunchYouTube(new Uri($"http://www.youtube.com/tv?v={videoId}"));
        }

        public async Task<string> LaunchYouTube(Uri uri)
        {
            return await LaunchApp("youtube.leanback.v4", uri);
        }

        public async Task<string> LaunchWebBrowser(Uri uri)
        {
            var requestMessage = new RequestMessage("ssap://system.launcher/open", new { target = uri.ToString() });
            var response = await _connection.SendCommandAsync(requestMessage);
            return (string) response.sessionId;
        }

        public async Task<string> CloseApp(string appId)
        {
            var requestMessage = new RequestMessage("ssap://system.launcher/close", new { id = appId });
            var response = await _connection.SendCommandAsync(requestMessage);
            return (string) response.sessionId;
        }
    }
}
