using System.Threading.Tasks;

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
            await _connection.SendCommandAsync(new RequestMessage("3d", "ssap://com.webos.service.tv.display/set3DOn"));
        }

        public async Task TurnOff3D()
        {
            await _connection.SendCommandAsync(new RequestMessage("3d", "ssap://com.webos.service.tv.display/set3DOff"));
        }

        public async Task<bool> IsTurnedOn3D()
        {
            //Response: { returnValue: true,  status3D: { status: true, pattern: ’2Dto3D’ } }
            var requestMessage = new RequestMessage("status3D", "ssap://com.webos.service.tv.display/get3DStatus");
            var response = await _connection.SendCommandAsync(requestMessage);
            return (bool) response.status3D.status;
        }
    }
}
