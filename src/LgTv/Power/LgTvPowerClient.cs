using System.Threading.Tasks;

namespace LgTv.Power
{
    internal class LgTvPowerClient : ILgTvPowerClient
    {
        private readonly ILgTvConnection _connection;

        public LgTvPowerClient(
            ILgTvConnection connection)
        {
            _connection = connection;
        }
        
        public async Task TurnOff()
        {
            await _connection.SendCommandAsync(new RequestMessage("", "ssap://system/turnOff"));
        }
    }
}
