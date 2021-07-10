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
        private const string ProxyHost = "ENTER_HOSTNAME_OR_IP_ADDRESS";
        private const int ProxyPort = 8080; // ENTER PORT NUMBER

        private const string TvHost = "ENTER_HOSTNAME_OR_IP_ADDRESS";

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddSingleton<IClientKeyStore, LocalStorageClientKeyStore>();
            builder.Services.AddSingleton<Func<ILgTvConnection>>(() => new LgTvConnection());
            builder.Services.AddSingleton<ILgTvClient>(provider => new LgTvClient(
                provider.GetService<Func<ILgTvConnection>>(),
                provider.GetService<IClientKeyStore>(),
                new LgTvClientConfiguration(
                    new HostConfiguration(true, ProxyHost, ProxyPort),
                    new HostConfiguration(false, TvHost, LgTvClient.DefaultInsecurePort))));

            await builder.Build().RunAsync();
        }
    }
}
