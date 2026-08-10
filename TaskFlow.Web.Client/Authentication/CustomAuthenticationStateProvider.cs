using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using TaskFlow.Web.Client.Services.Storage;

namespace TaskFlow.Web.Client.Authentication;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IStorageService _storageService;
    private readonly IJwtParser _jwtParser;

    public CustomAuthenticationStateProvider(IStorageService storageService, IJwtParser jwtParser)
    {
        _storageService = storageService;
        _jwtParser = jwtParser;
    }

    // Essa classe gerencia o estado de autenticação da aplicação.
    // Ela informa ao Blazor quem é o usuário atual,
    // se ele está autenticado e cria o ClaimsPrincipal utilizado internamente.
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _storageService.GetItemAsync<string>("token");
        if (string.IsNullOrWhiteSpace(token))
        {
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            return new AuthenticationState(anonymous);
        }
        var principal = _jwtParser.CreateClaimsPrincipal(token);
        return new AuthenticationState(principal);
    }

    public void MarkUserAsAuthenticated(string token)
    {
        var principal = _jwtParser.CreateClaimsPrincipal(token);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }

    public void MarkUserAsLoggedOut()
    {
        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
    }
}