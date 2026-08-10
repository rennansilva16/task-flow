using TaskFlow.Shared.Requests;

namespace TaskFlow.Web.Client.Authentication.Interfaces;

public interface IAuthenticationService
{
    public Task<bool> LoginAsync(LoginRequest request);
    public Task<bool> IsAuthenticatedAsync();
    public Task LogoutAsync();
}