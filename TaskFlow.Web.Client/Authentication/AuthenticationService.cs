using TaskFlow.Shared.Requests;

namespace TaskFlow.Web.Client.Authentication;

public class AuthenticationService
{
    // Ela faz: Login =>  Recebe LoginResponse => Salva Token => Salva Usuário =>  Atualiza AuthenticationState => Redireciona
    // Ela também faz: Logout => Apaga Token => Apaga Sessão => Vai para Login
    private readonly UserServiceAPI _userServiceAPI;

    public AuthenticationService(UserServiceAPI userServiceAPI)
    {
        _userServiceAPI = userServiceAPI;
    }

    public async Task<bool> LoginAsync(LoginRequest request)
    {
        var response = await _userServiceAPI.LoginAsync(request);

        if (response != null)
        {
            // Salvar o token e o usuário no local storage ou session storage
            // Atualizar o estado de autenticação
            return true;
        }
        return false;
    }
}