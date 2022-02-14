using System;
using System.Threading.Tasks;

namespace LgTv.Clients
{
    public interface ILgTvClientController
    {
        event EventHandler Connected;

        event EventHandler Disconnected;

        bool IsConnected { get; }

        ILgTvClient Client { get; }

        Task Connect(
            HostConfiguration tvHostConfiguration,
            HostConfiguration proxyHostConfiguration = null);

        void Disconnect();
    }
}
