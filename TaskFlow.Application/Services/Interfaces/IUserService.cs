using TaskFlow.Shared.Requests;

namespace TaskFlow.Application.Services.Interfaces;

public interface IUserService
{
    public Task<UserResponse> CreateUserAsync(CreateUserRequest request);
    public Task<LoginResponse?> LoginAsync(LoginRequest request);
}