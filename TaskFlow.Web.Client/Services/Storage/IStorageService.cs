namespace TaskFlow.Web.Client.Services.Storage;

public interface IStorageService
{
     Task SetItemAsync<T>(string key, T value);

    Task<T?> GetItemAsync<T>(string key);

    Task RemoveItemAsync(string key);
}