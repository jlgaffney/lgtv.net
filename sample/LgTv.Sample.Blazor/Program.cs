using LgTv.Clients;
using LgTv.Connections;
using LgTv.Sample.Blazor;
using LgTv.Sample.Blazor.Services;
using LgTv.Stores;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseAddress = new Uri(builder.HostEnvironment.BaseAddress);

builder.Services.AddMudServices();

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = baseAddress });
builder.Services.Configure<HostConfiguration>(x => builder.Configuration.GetSection("Tv").Bind(x));
builder.Services.Configure<ProxyHostConfiguration>(x =>
{
    builder.Configuration.GetSection("Proxy").Bind(x);
    if (string.IsNullOrEmpty(x.Endpoint.Host))
    {
        x.Endpoint.Host = baseAddress.Host;
    }
});
builder.Services.AddSingleton<IClientKeyStore, LocalStorageClientKeyStore>();
builder.Services.AddSingleton<Func<ILgTvConnection>>(() => new LgTvConnection());
builder.Services.AddSingleton<LgTvClientController.ClientFactory>(provider =>
{
    return (tvHostConfig, proxyHostConfig) => new LgTvClient(
        provider.GetService<Func<ILgTvConnection>>(),
        provider.GetService<IClientKeyStore>(),
        new LgTvClientConfiguration(
            tvHostConfig ?? provider.GetService<IOptions<HostConfiguration>>().Value,
            proxyHostConfig ?? provider.GetService<IOptions<ProxyHostConfiguration>>().Value.Endpoint));
});
builder.Services.AddSingleton<ILgTvClientController, LgTvClientController>();

await builder.Build().RunAsync();
