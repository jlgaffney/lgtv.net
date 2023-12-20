using LgTv;
using LgTv.Extensions;
using LgTv.Sample.Blazor;
using LgTv.Sample.Blazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseAddress = new Uri(builder.HostEnvironment.BaseAddress);

builder.Services
    .AddScoped(_ => new HttpClient { BaseAddress = baseAddress })
    .Configure<ProxyHostConfiguration>(x =>
    {
        builder.Configuration.GetSection("Proxy").Bind(x);
        if (string.IsNullOrEmpty(x.Endpoint.Host))
        {
            x.Endpoint.Host = baseAddress.Host;
        }
    })
    .AddLgTvClient<LocalStorageClientKeyStore>((configuration, provider) =>
    {
        configuration.Tv = builder.Configuration.GetSection("Tv").Get<HostConfiguration>();

        configuration.Proxy = provider.GetService<IOptions<ProxyHostConfiguration>>()?.Value.Endpoint;
    })
    .AddMudServices();

await builder.Build().RunAsync();
