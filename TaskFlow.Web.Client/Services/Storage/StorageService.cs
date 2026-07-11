using System.Text.Json;
using Microsoft.JSInterop;

namespace TaskFlow.Web.Client.Services.Storage;

public class StorageService : IStorageService
{
    private readonly IJSRuntime _jsRuntime;

    public StorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task SetItemAsync<T>(string key, T value)
    {
        var json = JsonSerializer.Serialize(value);

        await _jsRuntime.InvokeVoidAsync(
            "localStorage.setItem",
            key,
            json);
    }

    public async Task<T?> GetItemAsync<T>(string key)
    {
        var json = await _jsRuntime.InvokeAsync<string?>(
            "localStorage.getItem",
            key);

        if (string.IsNullOrWhiteSpace(json))
            return default;

        return JsonSerializer.Deserialize<T>(json);
    }

    public async Task RemoveItemAsync(string key)
    {
        await _jsRuntime.InvokeVoidAsync(
            "localStorage.removeItem",
            key);
    }
}