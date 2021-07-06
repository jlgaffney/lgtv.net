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


            var mouse = await client.GetMouse();
            

            // Volume control
            await client.VolumeDown();
            await client.VolumeUp();


            // Playback control
            await client.Pause();
            await client.Play();


            var apps = await client.GetApps();


            (await client.GetMouse()).SendButton(ButtonType.UP);
            (await client.GetMouse()).SendButton(ButtonType.LEFT);
            (await client.GetMouse()).SendButton(ButtonType.RIGHT);
            (await client.GetMouse()).SendButton(ButtonType.DOWN);


            await client.TurnOff();
        }
    }
}
