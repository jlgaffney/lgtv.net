using AspNetCore.WebSocketProxy;
using LgTv.Extensions;
using LgTv.Networking;
using LgTv.Scanning;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCors()
    .AddNetworkServices();

var app = builder.Build();

// Global CORS policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.MapGet("/scan", async (ITvScanner scanner) =>
{
    var devices = await scanner.GetDevices();

    return devices ?? Enumerable.Empty<Device>();
});

app.MapPost("/wol/{ipAddress}", async (IMacAddressResolver macAddressResolver, IWakeOnLan wakeOnLan, string ipAddress) =>
{
    var macAddress = await macAddressResolver.ResolveAsync(ipAddress);

    await wakeOnLan.SendMagicPacketAsync(macAddress);
});

app.UseWebSocketProxy();

app.Run();
