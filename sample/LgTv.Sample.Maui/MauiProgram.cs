using System.ComponentModel;
using CommunityToolkit.Maui;
using MauiIcons.Material;
using Microsoft.Extensions.Logging;

namespace LgTv.Sample.Maui;

public static class MauiProgram
{
    private const string ClientKeyStoreFileName = "client-keys.json";

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMaterialMauiIcons()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddServices();

        var app = builder.Build();

        ServiceLocator.Initialize(app.Services);

        return app;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<INavigationService, NavigationService>();
        services.AddScoped<AppShell>();

        var viewModelTypes = typeof(MauiProgram).Assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(INotifyPropertyChanged)) && !x.IsAbstract);

        foreach (var type in viewModelTypes)
        {
            services.AddTransient(type);
        }

        var clientKeyStoreFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, ClientKeyStoreFileName);
        services.AddLgTvClient(configuration =>
        {
            configuration.ClientKeyStoreFilePath = clientKeyStoreFilePath;
        });

        return services;
    }
}
