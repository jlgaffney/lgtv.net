using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using LgTv.SampleBlazor.Services;
using Microsoft.Extensions.Configuration;

namespace LgTv.SampleBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddSingleton<IClientKeyStore, LocalStorageClientKeyStore>();
            builder.Services.AddSingleton<Func<ILgTvConnection>>(() => new LgTvConnection());
            builder.Services.AddSingleton<ILgTvClient>(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();

                return new LgTvClient(
                    provider.GetService<Func<ILgTvConnection>>(),
                    provider.GetService<IClientKeyStore>(),
                    new LgTvClientConfiguration(
                        configuration.GetSection("Tv").Get<HostConfiguration>(),
                        configuration.GetSection("Proxy").Get<HostConfiguration>()));
            });

            await builder.Build().RunAsync();
        }
    }
}
