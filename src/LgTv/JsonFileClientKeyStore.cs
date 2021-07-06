using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace LgTv
{
    public class JsonFileClientKeyStore : IClientKeyStore
    {
        private const string DefaultJsonContents = "{\n}";
        private const string ClientKeyJsonPropertyName = "client-key";

        private readonly string _clientKeyStoreJsonFilePath;

        private readonly string _hostname;

        public JsonFileClientKeyStore(
            string clientKeyStoreJsonFilePath,
            string hostname)
        {
            _clientKeyStoreJsonFilePath = clientKeyStoreJsonFilePath;

            _hostname = hostname;
        }

        public async Task<string> GetClientKey()
        {
            await Task.CompletedTask;

            if (!File.Exists(_clientKeyStoreJsonFilePath))
            {
                return null;
            }

            var clientKeyStoreFileContents = File.ReadAllText(_clientKeyStoreJsonFilePath);

            if (string.IsNullOrWhiteSpace(clientKeyStoreFileContents))
            {
                clientKeyStoreFileContents = DefaultJsonContents;
            }

            var json = JObject.Parse(clientKeyStoreFileContents);

            var key = json[_hostname]?[ClientKeyJsonPropertyName]?.ToObject<string>();

            return key;
        }

        public async Task SetClientKey(string key)
        {
            await Task.CompletedTask;

            if (!File.Exists(_clientKeyStoreJsonFilePath))
            {
                File.WriteAllText(_clientKeyStoreJsonFilePath, DefaultJsonContents);
            }

            var clientKeyStoreFileContents = File.ReadAllText(_clientKeyStoreJsonFilePath);

            if (string.IsNullOrWhiteSpace(clientKeyStoreFileContents))
            {
                clientKeyStoreFileContents = DefaultJsonContents;
            }

            var json = JObject.Parse(clientKeyStoreFileContents);

            json[_hostname] = new JObject
            {
                [ClientKeyJsonPropertyName] = key
            };

            File.WriteAllText(_clientKeyStoreJsonFilePath, json.ToString());
        }
    }
}
