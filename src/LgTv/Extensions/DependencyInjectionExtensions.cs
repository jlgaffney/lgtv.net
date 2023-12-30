using LgTv.Networking;
using LgTv.Scanning;
using LgTv.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LgTv.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddNetworkServices(this IServiceCollection services)
    {
        services.AddSingleton<IIPAddressResolver, IPAddressResolver>();
        services.AddSingleton<IMacAddressResolver, MacAddressResolver>();
        services.AddSingleton<IWakeOnLan, WakeOnLan>();
        services.AddSingleton<ITvScanner, TvScanner>();

        return services;
    }

    public static IServiceCollection AddLgTvClient<TClientKeyStore>(
        this IServiceCollection services,
        Action<ClientConfiguration> configure)
        where TClientKeyStore : class, IClientKeyStore
    {
        return services.AddLgTvClient<TClientKeyStore>((configuration, _) => configure(configuration));
    }

    public static IServiceCollection AddLgTvClient<TClientKeyStore>(
        this IServiceCollection services,
        Action<ClientConfiguration, IServiceProvider> configure)
        where TClientKeyStore : class, IClientKeyStore
    {
        return services
            .AddDefaultServices()
            .AddSingleton<IClientKeyStore, TClientKeyStore>()
            .AddOptions<ClientConfiguration>().Configure(configure)
            .Services;
    }

    public static IServiceCollection AddLgTvClient(
        this IServiceCollection services,
        Action<ClientConfiguration> configure)
    {
        return services.AddLgTvClient((configuration, _) => configure(configuration));
    }

    public static IServiceCollection AddLgTvClient(
        this IServiceCollection services,
        Action<ClientConfiguration, IServiceProvider> configure)
    {
        return services
            .AddDefaultServices()
            .AddOptions<ClientConfiguration>().Configure(configure).Services
            .AddSingleton<IClientKeyStore>(provider =>
            {
                var configuration = provider.GetRequiredService<IOptions<ClientConfiguration>>();
                if (string.IsNullOrWhiteSpace(configuration.Value.ClientKeyStoreFilePath))
                {
                    throw new InvalidOperationException("Client key store file path is not configured.");
                }

                return new JsonFileClientKeyStore(configuration.Value.ClientKeyStoreFilePath);
            });
    }

    private static IServiceCollection AddDefaultServices(this IServiceCollection services)
    {
        services.AddNetworkServices();

        services.AddSingleton<Func<IConnection>>(() => new Connection());
        services.AddSingleton<ClientController.ClientFactory>(provider =>
        {
            var configuration = provider.GetRequiredService<IOptions<ClientConfiguration>>();

            return (tvHostConfig, proxyHostConfig) => new Client(
                provider.GetRequiredService<IIPAddressResolver>(),
                provider.GetRequiredService<IMacAddressResolver>(),
                provider.GetRequiredService<IWakeOnLan>(),
                provider.GetRequiredService<Func<IConnection>>(),
                provider.GetRequiredService<IClientKeyStore>(),
                tvHostConfig ?? configuration.Value.Tv,
                proxyHostConfig ?? configuration.Value.Proxy);
        });
        services.AddSingleton<IClientController, ClientController>();

        return services;
    }
}
