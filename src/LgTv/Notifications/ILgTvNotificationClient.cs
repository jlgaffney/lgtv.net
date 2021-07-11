using System.Threading.Tasks;

namespace LgTv.Notifications
{
    public interface ILgTvNotificationClient
    {
        Task ShowToast(string message);

        Task ShowToast(string message, byte[] iconData, string iconExtension);
    }
}