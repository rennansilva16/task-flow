using TaskFlow.Shared.Requests;
using TaskFlow.Web.Client.Services.Storage;

namespace TaskFlow.Web.Client.Authentication;

public class AuthenticationService
{
    // Ela faz: Login =>  Recebe LoginResponse => Salva Token => Salva Usuário =>  Atualiza AuthenticationState => Redireciona
    // Ela também faz: Logout => Apaga Token => Apaga Sessão => Vai para Login
    private readonly UserServiceAPI _userServiceAPI;
    private readonly IStorageService _storageService;

    public AuthenticationService(UserServiceAPI userServiceAPI, IStorageService storageService)
    {
        _userServiceAPI = userServiceAPI;
        _storageService = storageService;
    }

    public async Task<bool> LoginAsync(LoginRequest request)
    {
        LoginResponse? response = await _userServiceAPI.LoginAsync(request);

        if (response != null)
        {
            await _storageService.SetItemAsync("token", response.Token);
            var valor = await _storageService.GetItemAsync<string>("token");
            Console.WriteLine($"Token armazenado: {valor}");

            
            // Salvar o token e o usuário no local storage ou session storage
            // Atualizar o estado de autenticação
            return true;
        }
        return false;
    }
}