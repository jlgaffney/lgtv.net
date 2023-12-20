namespace LgTv.Features.Notifications;

public class NotificationClient(IConnection connection) : INotificationClient
{
    private readonly IConnection _connection = connection ?? throw new ArgumentNullException(nameof(connection));

    public async Task<string> ShowToast(string message)
    {
        var requestMessage = new RequestMessage(Commands.ShowToast.Uri, new { message });
        var response = await _connection.SendCommandAsync(requestMessage);
        return response.toastId;
    }

    public async Task<string> ShowToast(string message, byte[] iconData, string iconExtension)
    {
        // TODO Figure out why this is not working
        var iconDataBase64 = Convert.ToBase64String(iconData);
        var requestMessage = new RequestMessage(Commands.ShowToast.Uri, new { iconData = iconDataBase64, iconExtension, message });
        var response = await _connection.SendCommandAsync(requestMessage);
        return response.toastId;
    }

    public async Task CloseToast(string toastId)
    {
        // TODO Figure out why this is not working
        var requestMessage = new RequestMessage(Commands.CloseToast.Uri, new { toastId });
        await _connection.SendCommandAsync(requestMessage);
    }
}
