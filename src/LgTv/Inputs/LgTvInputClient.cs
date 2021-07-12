using System.Collections.Generic;
using System.Threading.Tasks;

namespace LgTv.Inputs
{
    internal class LgTvInputClient : ILgTvInputClient
    {
        private readonly ILgTvConnection _connection;

        public LgTvInputClient(
            ILgTvConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<ExternalInput>> GetInputs()
        {
            var requestMessage = new RequestMessage("input", "ssap://tv/getExternalInputList");
            var response = await _connection.SendCommandAsync(requestMessage);

            var inputs = new List<ExternalInput>();
            foreach (var device in response.devices)
            {
                inputs.Add(new ExternalInput
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
            var requestMessage = new RequestMessage("ssap://tv/switchInput", new { inputId = id });
            await _connection.SendCommandAsync(requestMessage);
        }
    }
}
