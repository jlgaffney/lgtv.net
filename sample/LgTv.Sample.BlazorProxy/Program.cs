using System.Linq;
using AspNetCore.WebSocketProxy;
using LgTv.Networking;
using LgTv.Scanning;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ILgTvScanner, LgTvScanner>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/scan", async (ILgTvScanner scanner) =>
{
    var devices = await scanner.GetDevices();

    return devices ?? Enumerable.Empty<Device>();
});

app.MapPost("/wakeonlan/{ipAddress}", async (string ipAddress) =>
{
    var macAddress = MacAddressResolver.GetMacAddress(ipAddress);

    await WakeOnLan.SendMagicPacket(macAddress);
});

app.UseWebSocketProxy();

app.Run();