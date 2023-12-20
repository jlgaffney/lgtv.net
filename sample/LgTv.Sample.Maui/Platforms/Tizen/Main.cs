using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace LgTv.Sample.Maui.Platforms.Tizen;

internal class Program : MauiApplication
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    static void Main(string[] args)
    {
        var app = new Program();
        app.Run(args);
    }
}
