using AspNetCore.WebSocketProxy;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseWebSocketProxy();

app.Run();