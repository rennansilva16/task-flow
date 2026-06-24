using System.Net.Http.Json;

public class UserService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "api/usuario";

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserResponse?> CreateUserAsync(CreateUserRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseUrl, request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<UserResponse>();
        }
        return null;
    }
}