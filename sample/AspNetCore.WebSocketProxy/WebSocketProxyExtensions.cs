using System.Net.WebSockets;
using Microsoft.AspNetCore.Builder;

namespace AspNetCore.WebSocketProxy;

public static class WebSocketProxyExtensions
{
    public static IApplicationBuilder UseWebSocketProxy(this IApplicationBuilder app)
    {
        app.UseWebSockets();

        app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest && context.Request.Path.HasValue)
            {
                // TODO Make proxy more configurable
                var path = context.Request.Path.Value?.Trim('/');

                if (!string.IsNullOrWhiteSpace(path))
                {
                    var clientSocket = await context.WebSockets.AcceptWebSocketAsync();

                    var serverSocket = new ClientWebSocket();

                    serverSocket.Options.RemoteCertificateValidationCallback = (sender, cert, chain, errors) => true;

                    await serverSocket.ConnectAsync(new Uri(path), CancellationToken.None);

                    await Task.WhenAll(
                        ForwardMessages(clientSocket, serverSocket),
                        ForwardMessages(serverSocket, clientSocket));

                    return;
                }
            }

            await next();
        });

        return app;
    }

    private static async Task ForwardMessages(WebSocket producer, WebSocket consumer)
    {
        var buffer = new byte[4096];
        var result = await producer.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        while (!result.CloseStatus.HasValue)
        {
            await consumer.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
            result = await producer.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }
        await consumer.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }
}
