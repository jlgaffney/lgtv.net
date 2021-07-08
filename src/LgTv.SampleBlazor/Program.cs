using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using LgTv.SampleBlazor.Services;

namespace LgTv.SampleBlazor
{
    public class Program
    {
        private const string TvHostname = "ENTER_HOSTNAME_OR_IP_ADDRESS";
        private const int TvPort = 3000;

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddSingleton<IClientKeyStore, LocalStorageClientKeyStore>();
            builder.Services.AddSingleton<Func<ILgTvConnection>>(() => new LgTvConnection());
            builder.Services.AddSingleton<ILgTvClient>(provider => new LgTvClient(provider.GetService<Func<ILgTvConnection>>(), provider.GetService<IClientKeyStore>(), TvHostname, TvPort));

            await builder.Build().RunAsync();
        }
    }
}
