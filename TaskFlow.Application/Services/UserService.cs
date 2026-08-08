using TaskFlow.Application.Identity;
using TaskFlow.Application.Persistence;
using TaskFlow.Shared.Requests;

namespace TaskFlow.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUserRepository userRepository, IJwtService jwtService, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserResponse> CreateUserAsync(CreateUserRequest request)
    {
        var usuario = new Usuario
        {
            Nome = request.Nome,
            Login = request.Login,
            Senha = _passwordHasher.Hash(request.Senha)
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
        // Validar senha
        // Criar TOken
        // Retornar LoginResponse com o token e informações do usuário
        var usuario = await _userRepository.GetUserByLoginAsync(request.Login);

        if (usuario == null)
        {
            return null;
        }

        var senhaValida = usuario.Senha.StartsWith("PBKDF2-SHA512$", StringComparison.Ordinal)
            ? _passwordHasher.Verify(usuario.Senha, request.Password)
            : usuario.Senha == request.Password;

        if (!senhaValida)
        {
            return null; // Retorna null se o usuário não for encontrado ou a senha estiver incorreta
        }

        string token = _jwtService.GenerateToken(usuario);

        UserResponse userResponse = new UserResponse
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Login = usuario.Login
        };

        LoginResponse loginResponse = new LoginResponse
        {
            Usuario = userResponse,
            Token = token
        };

        return loginResponse;
    }
}
