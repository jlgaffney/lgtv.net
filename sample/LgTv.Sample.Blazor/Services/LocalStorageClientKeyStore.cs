using LgTv.Stores;
using Microsoft.JSInterop;

namespace LgTv.Sample.Blazor.Services;

public class LocalStorageClientKeyStore(
    IJSRuntime javaScriptRuntime)
    : IClientKeyStore
{
    public async Task<string> GetClientKey(string ipAddress)
    {
        return await javaScriptRuntime.InvokeAsync<string>("localStorage.getItem", GetLocalStorageKey(ipAddress));
    }

    public async Task SetClientKey(string ipAddress, string key)
    {
        await javaScriptRuntime.InvokeVoidAsync("localStorage.setItem", GetLocalStorageKey(ipAddress), key);
    }

    private static string GetLocalStorageKey(string ipAddress)
    {
        return FormattableString.Invariant($"{ipAddress}-client-key");
    }
}
