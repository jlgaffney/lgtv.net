using System;
using System.Threading.Tasks;
using LgTv.Stores;
using Microsoft.JSInterop;

namespace LgTv.Sample.Blazor.Services
{
    public class LocalStorageClientKeyStore : IClientKeyStore
    {
        private readonly IJSRuntime _javaScriptRuntime;

        public LocalStorageClientKeyStore(
            IJSRuntime javaScriptRuntime)
        {
            _javaScriptRuntime = javaScriptRuntime;
        }

        public async Task<string> GetClientKey(string ipAddress)
        {
            return await _javaScriptRuntime.InvokeAsync<string>("localStorage.getItem", GetLocalStorageKey(ipAddress));
        }

        public async Task SetClientKey(string ipAddress, string key)
        {
            await _javaScriptRuntime.InvokeVoidAsync("localStorage.setItem", GetLocalStorageKey(ipAddress), key);
        }

        private static string GetLocalStorageKey(string ipAddress)
        {
            return FormattableString.Invariant($"{ipAddress}-client-key");
        }
    }
}
