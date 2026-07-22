using TaskFlow.Shared.Requests;

namespace TaskFlow.Web.Client.Services.Interfaces;
public interface IUserServiceAPI
{
    public Task<UserResponse?> CreateUserAsync(CreateUserRequest request);

    public Task<LoginResponse?> LoginAsync(LoginRequest request);
}