﻿using System.IO;
using System.Threading.Tasks;
using LgTv.Mouse;

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
            var client = new LgTvClient(() => new LgTvConnection(), new JsonFileClientKeyStore(ClientKeyStoreFilePath, TvHostname), TvHostname, TvPort);
            
            await client.Connect();
            await client.MakeHandShake();


            // Volume control
            await client.Volume.VolumeDown();
            await client.Volume.VolumeUp();


            // Playback control
            await client.Playback.Pause();
            await client.Playback.Play();


            var apps = await client.Apps.GetApps();


            using (var mouse = await client.GetMouse())
            {
                await mouse.SendButton(ButtonType.UP);
                await mouse.SendButton(ButtonType.LEFT);
                await mouse.SendButton(ButtonType.RIGHT);
                await mouse.SendButton(ButtonType.DOWN);
            }


            await client.Power.TurnOff();
        }
    }
}
