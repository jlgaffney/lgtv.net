using LgTv.Connections;

namespace LgTv.Clients.Display;

internal class LgTvDisplayClient(ILgTvConnection connection) : ILgTvDisplayClient
{
    public async Task TurnOn3D()
    {
        await connection.SendCommandAsync(new RequestMessage(LgTvCommands.Set3DOn.Prefix, LgTvCommands.Set3DOn.Uri));
    }

    public async Task TurnOff3D()
    {
        await connection.SendCommandAsync(new RequestMessage(LgTvCommands.Set3DOff.Prefix, LgTvCommands.Set3DOff.Uri));
    }

    public async Task<bool> Is3DTurnedOn()
    {
        //Response: { returnValue: true,  status3D: { status: true, pattern: ’2Dto3D’ } }
        var requestMessage = new RequestMessage(LgTvCommands.Get3DStatus.Prefix, LgTvCommands.Get3DStatus.Uri);
        var response = await connection.SendCommandAsync(requestMessage);
        return (bool) response.status3D.status;
    }
}