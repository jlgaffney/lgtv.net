using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LgTv.Connections;

namespace LgTv.Clients.Apps
{
    internal class LgTvAppClient : ILgTvAppClient
    {
        private const string YouTubeAppId = "youtube.leanback.v4";
        private const string YouTubeVideoUrlPrefix = "http://www.youtube.com/tv?v=";

        private readonly ILgTvConnection _connection;

        public LgTvAppClient(
            ILgTvConnection connection)
        {
            _connection = connection;
        }

        public async Task<ForegroundAppInfo> GetForegroundAppInfo()
        {
            var requestMessage = new RequestMessage(LgTvCommands.GetForegroundAppInfo.Uri);
            var response = await _connection.SendCommandAsync(requestMessage);

            return new ForegroundAppInfo
            {
                AppId = response.appId,
                WindowId = response.windowId,
                ProcessId = response.processId
            };
        }

        public async Task<App> GetApp(string id)
        {
            return (await GetApps()).FirstOrDefault(x => string.Equals(id, x.Id, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<IEnumerable<App>> GetApps()
        {
            var requestMessage = new RequestMessage(LgTvCommands.GetApps.Prefix, LgTvCommands.GetApps.Uri);
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

        public async Task<string> LaunchApp(string id, Uri uri = null)
        {
            dynamic requestPayload = new { id = id };
            if (uri != null)
            {
                requestPayload.@params = new { contentTarget = uri.ToString() };
            }

            var requestMessage = new RequestMessage(LgTvCommands.LaunchApp.Uri, (object) requestPayload);
            var response = await _connection.SendCommandAsync(requestMessage);
            return (string) response.sessionId;
        }

        public async Task<string> LaunchYouTube(string videoId)
        {
            return await LaunchYouTube(new Uri(FormattableString.Invariant($"{YouTubeVideoUrlPrefix}{videoId}")));
        }

        public async Task<string> LaunchYouTube(Uri uri)
        {
            return await LaunchApp(YouTubeAppId, uri);
        }

        public async Task<string> LaunchWebBrowser(Uri uri)
        {
            var requestMessage = new RequestMessage(LgTvCommands.OpenApp.Uri, new { target = uri.ToString() });
            var response = await _connection.SendCommandAsync(requestMessage);
            return (string) response.sessionId;
        }

        public async Task<string> CloseApp(string id)
        {
            var requestMessage = new RequestMessage(LgTvCommands.CloseApp.Uri, new { id = id });
            var response = await _connection.SendCommandAsync(requestMessage);
            return (string) response.sessionId;
        }
    }
}
