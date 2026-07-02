using TaskFlow.Infrastructure.Repositories;
using TaskFlow.Shared.Requests;

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

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var usuario = await _userRepository.GetUserByLoginAsync(request.Login);

        if (usuario == null || usuario.Senha != request.Password)
        {
            return null; // Retorna null se o usuário não for encontrado ou a senha estiver incorreta
        }

        UserResponse userResponse = new UserResponse
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Login = usuario.Login
        };

        LoginResponse loginResponse = new LoginResponse
        {
            Usuario = userResponse
        };

        return loginResponse;
    }
}