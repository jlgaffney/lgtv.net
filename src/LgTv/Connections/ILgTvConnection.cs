using System;
using System.Threading.Tasks;

namespace LgTv.Connections
{
    public interface ILgTvConnection : IDisposable
    {
        Task<bool> Connect(Uri uri);

        Task SendMessageAsync(string message);

        Task<dynamic> SendCommandAsync(string message);

        Task<dynamic> SendCommandAsync(RequestMessage message);

        Task CloseAsync();
    }
}
