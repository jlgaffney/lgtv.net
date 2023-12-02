using LgTv.Connections;

namespace LgTv.Clients.Notifications;

public class LgTvNotificationClient(ILgTvConnection connection) : ILgTvNotificationClient
{
    public async Task<string> ShowToast(string message)
    {
        var requestMessage = new RequestMessage(LgTvCommands.ShowToast.Uri , new { message = message });
        var response = await connection.SendCommandAsync(requestMessage);
        return response.toastId;
    }

    public async Task<string> ShowToast(string message, byte[] iconData, string iconExtension)
    {
        // TODO Figure out why this is not working
        var iconDataBase64 = Convert.ToBase64String(iconData);
        var requestMessage = new RequestMessage(LgTvCommands.ShowToast.Uri, new { iconData = iconDataBase64, iconExtension = iconExtension, message = message });
        var response = await connection.SendCommandAsync(requestMessage);
        return response.toastId;
    }

    public async Task CloseToast(string toastId)
    {
        // TODO Figure out why this is not working
        var requestMessage = new RequestMessage(LgTvCommands.CloseToast.Uri, new { toastId = toastId });
        await connection.SendCommandAsync(requestMessage);
    }
}
