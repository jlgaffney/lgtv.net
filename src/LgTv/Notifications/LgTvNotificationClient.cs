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

        public async Task<string> ShowToast(string message)
        {
            var requestMessage = new RequestMessage("ssap://system.notifications/createToast", new { message = message });
            var response = await _connection.SendCommandAsync(requestMessage);
            return response.toastId;
        }

        public async Task<string> ShowToast(string message, byte[] iconData, string iconExtension)
        {
            // TODO Figure out why this is not working
            var iconDataBase64 = Convert.ToBase64String(iconData);
            var requestMessage = new RequestMessage("ssap://system.notifications/createToast", new { iconData = iconDataBase64, iconExtension = iconExtension, message = message });
            var response = await _connection.SendCommandAsync(requestMessage);
            return response.toastId;
        }

        public async Task CloseToast(string toastId)
        {
            // TODO Figure out why this is not working
            var requestMessage = new RequestMessage("ssap://system.notifications/closeToast", new { toastId = toastId });
            await _connection.SendCommandAsync(requestMessage);
        }
    }
}
