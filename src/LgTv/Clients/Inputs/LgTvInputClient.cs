using System.Collections.Generic;
using System.Threading.Tasks;
using LgTv.Connections;

namespace LgTv.Clients.Inputs
{
    internal class LgTvInputClient : ILgTvInputClient
    {
        private readonly ILgTvConnection _connection;

        public LgTvInputClient(
            ILgTvConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Input>> GetInputs()
        {
            var requestMessage = new RequestMessage(LgTvCommands.GetInputs.Prefix, LgTvCommands.GetInputs.Uri);
            var response = await _connection.SendCommandAsync(requestMessage);

            var inputs = new List<Input>();
            foreach (var device in response.devices)
            {
                inputs.Add(new Input
                {
                    Id = device.id,
                    Label = device.label,
                    Icon = device.icon,
                    Connected = (bool) device.connected
                });
            }

            return inputs;
        }

        public async Task SetInput(string id)
        {
            var requestMessage = new RequestMessage(LgTvCommands.SetInput.Uri, new { inputId = id });
            await _connection.SendCommandAsync(requestMessage);
        }
    }
}
