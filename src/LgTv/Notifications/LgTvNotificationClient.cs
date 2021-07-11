using System;
using System.Threading.Tasks;

namespace LgTv.Notifications
{
    public class LgTvNotificationClient : ILgTvNotificationClient
    {
        private readonly ILgTvConnection _connection;

        public LgTvNotificationClient(
            ILgTvConnection connection)
        {
            _connection = connection;
        }

        public async Task ShowToast(string message)
        {
            var requestMessage = new RequestMessage("ssap://system.notifications/createToast", new { message = message });
            await _connection.SendCommandAsync(requestMessage);
        }

        public async Task ShowToast(string message, byte[] iconData, string iconExtension)
        {
            var iconDataBase64 = Convert.ToBase64String(iconData);
            var requestMessage = new RequestMessage("ssap://system.notifications/createToast", new { iconData = iconDataBase64, iconExtension = iconExtension, message = message });
            await _connection.SendCommandAsync(requestMessage);
        }
    }
}
