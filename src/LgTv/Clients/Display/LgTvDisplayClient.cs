using System.Threading.Tasks;
using LgTv.Connections;

namespace LgTv.Clients.Display
{
    internal class LgTvDisplayClient : ILgTvDisplayClient
    {
        private readonly ILgTvConnection _connection;

        public LgTvDisplayClient(
            ILgTvConnection connection)
        {
            _connection = connection;
        }

        public async Task TurnOn3D()
        {
            await _connection.SendCommandAsync(new RequestMessage(LgTvCommands.Set3DOn.Prefix, LgTvCommands.Set3DOn.Uri));
        }

        public async Task TurnOff3D()
        {
            await _connection.SendCommandAsync(new RequestMessage(LgTvCommands.Set3DOff.Prefix, LgTvCommands.Set3DOff.Uri));
        }

        public async Task<bool> IsTurnedOn3D()
        {
            //Response: { returnValue: true,  status3D: { status: true, pattern: ’2Dto3D’ } }
            var requestMessage = new RequestMessage(LgTvCommands.Get3DStatus.Prefix, LgTvCommands.Get3DStatus.Uri);
            var response = await _connection.SendCommandAsync(requestMessage);
            return (bool) response.status3D.status;
        }
    }
}
