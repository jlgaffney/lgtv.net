using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using LgTv.Clients;
using LgTv.Connections;
using LgTv.Sample.Blazor.Services;
using LgTv.Stores;
using Microsoft.Extensions.Configuration;

namespace LgTv.Sample.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient() { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();

                return new ProxyHostConfiguration(
                    configuration.GetSection("Proxy").Get<HostConfiguration>());
            });
            builder.Services.AddSingleton<IClientKeyStore, LocalStorageClientKeyStore>();
            builder.Services.AddSingleton<Func<ILgTvConnection>>(() => new LgTvConnection());
            builder.Services.AddSingleton<LgTvClientController.ClientFactory>(provider =>
            {
                return (tvHostConfig, proxyHostConfig) =>
                {
                    var configuration = provider.GetService<IConfiguration>();

                    return new LgTvClient(
                        provider.GetService<Func<ILgTvConnection>>(),
                        provider.GetService<IClientKeyStore>(),
                        new LgTvClientConfiguration(
                            tvHostConfig ?? configuration.GetSection("Tv").Get<HostConfiguration>(),
                            proxyHostConfig ?? configuration.GetSection("Proxy").Get<HostConfiguration>()));
                };
            });
            builder.Services.AddSingleton<ILgTvClientController, LgTvClientController>();

            await builder.Build().RunAsync();
        }
    }
}
