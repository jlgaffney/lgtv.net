using System.Threading.Tasks;

namespace LgTv.Notifications
{
    public interface ILgTvNotificationClient
    {
        /// <returns>Unique toast identifier</returns>
        Task<string> ShowToast(string message);

        /// <returns>Unique toast identifier</returns>
        Task<string> ShowToast(string message, byte[] iconData, string iconExtension);

        Task CloseToast(string toastId);
    }
}