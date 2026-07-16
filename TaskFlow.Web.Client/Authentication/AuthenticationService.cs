using TaskFlow.Shared.Requests;
using TaskFlow.Web.Client.Services.Storage;

namespace TaskFlow.Web.Client.Authentication;

public class AuthenticationService
{
    // Ela faz: Login =>  Recebe LoginResponse => Salva Token => Salva Usuário =>  Atualiza AuthenticationState => Redireciona
    // Ela também faz: Logout => Apaga Token => Apaga Sessão => Vai para Login
    private readonly UserServiceAPI _userServiceAPI;
    private readonly IStorageService _storageService;
    private readonly CustomAuthenticationStateProvider _authenticationStateProvider;

    public AuthenticationService(UserServiceAPI userServiceAPI, CustomAuthenticationStateProvider authenticationStateProvider, IStorageService storageService)
    {
        _userServiceAPI = userServiceAPI;
        _authenticationStateProvider = authenticationStateProvider;
        _storageService = storageService;
    }

    public async Task<bool> LoginAsync(LoginRequest request)
    {
        LoginResponse? response = await _userServiceAPI.LoginAsync(request);

        if (response != null)
        {
            // Salvar o token no armazenamento local
            await _storageService.SetItemAsync("token", response.Token);

            // Atualizar o estado de autenticação
            _authenticationStateProvider.MarkUserAsAuthenticated(response.Token);
            return true;
        }   
        return false;
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        return authState.User.Identity?.IsAuthenticated ?? false;
    }
}