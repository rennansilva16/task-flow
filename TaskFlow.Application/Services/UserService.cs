using TaskFlow.Infrastructure.Repositories;

namespace TaskFlow.Application.Services;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponse> CreateUserAsync(CreateUserRequest request)
    {
        var usuario = new Usuario
        {
            Nome = request.Nome,
            Login = request.Login,
            Senha = request.Senha
        };
        Usuario usuarioCriado = await _userRepository.CreateUserAsync(usuario);

        UserResponse userResponse = new UserResponse
        {
            Id = usuarioCriado.Id,
            Nome = usuarioCriado.Nome,
            Login = usuarioCriado.Login
        };
        return userResponse;
    }    
}