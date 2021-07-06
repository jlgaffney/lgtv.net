using System.IO;
using System.Threading.Tasks;

namespace LgTv.SampleApp
{
    public class Program
    {
        private const string ClientKeyStoreFileName = "client-keys.json";

        private static readonly string ClientKeyStoreFilePath = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), ClientKeyStoreFileName);

        private const string TvHostname = "ENTER_HOSTNAME_OR_IP_ADDRESS";
        private const int TvPort = 3000;

        public static async Task Main(string[] args)
        {
            // Initalization
            var client = new LgTvClient(new LgTvConnection(), new JsonFileClientKeyStore(ClientKeyStoreFilePath, TvHostname), TvHostname, TvPort);
            
            await client.Connect();
            await client.MakeHandShake();


            // Volume control
            await client.VolumeDown();
            await client.VolumeUp();


            // Playback control
            await client.Pause();
            await client.Play();


            var apps = await client.GetApps();


            using (var mouse = await client.GetMouse())
            {
                mouse.SendButton(ButtonType.UP);
                mouse.SendButton(ButtonType.LEFT);
                mouse.SendButton(ButtonType.RIGHT);
                mouse.SendButton(ButtonType.DOWN);
            }


            await client.TurnOff();
        }
    }
}
