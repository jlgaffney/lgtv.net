using System.Threading.Tasks;
using LgTv.Connections;

namespace LgTv.Clients.Inputs
{
    public class LgTvKeyboardClient : ILgTvKeyboardClient
    {
        private readonly ILgTvConnection _connection;

        public LgTvKeyboardClient(
            ILgTvConnection connection)
        {
            _connection = connection;
        }

        public async Task InsertText(string text, int replace = 0)
        {
            var requestMessage = new RequestMessage(LgTvCommands.InsertText.Uri, new { text, replace });
            await _connection.SendCommandAsync(requestMessage);
        }

        public async Task DeleteCharacters(int count = 1)
        {
            var requestMessage = new RequestMessage(LgTvCommands.DeleteCharacters.Uri, new { count });
            await _connection.SendCommandAsync(requestMessage);
        }

        public async Task SendEnterKey()
        {
            var requestMessage = new RequestMessage(LgTvCommands.SendEnterKey.Uri);
            await _connection.SendCommandAsync(requestMessage);
        }
    }
}
