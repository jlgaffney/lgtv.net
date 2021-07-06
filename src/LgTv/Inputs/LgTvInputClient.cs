using System.Collections.Generic;
using System.Linq;
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
            var results = await _connection.SendCommandAsync(requestMessage);

            var inputs = new List<ExternalInput>();
            foreach (var result in results)
            {
                inputs.Add(new ExternalInput(result.id, result.label)
                {
                    Icon = result.icon
                });
            }

            return inputs.OrderBy(x => x.Label);
        }

        public async Task SetInput(string id)
        {
            var requestMessage = new RequestMessage("ssap://tv/switchInput", new { inputId = id });
            await _connection.SendCommandAsync(requestMessage);
        }
    }
}
