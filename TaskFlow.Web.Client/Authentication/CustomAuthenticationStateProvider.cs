using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using TaskFlow.Web.Client.Services.Storage;

namespace TaskFlow.Web.Client.Authentication;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IStorageService _storageService;
    private const string AuthenticationType = "jwt"; // Tipo de autenticação, pode ser "jwt" ou outro valor que você preferir

    public CustomAuthenticationStateProvider(IStorageService storageService)
    {
        _storageService = storageService;
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
        var claims = ParseClaimsFromToken(token);


        var identity = new ClaimsIdentity(claims, AuthenticationType); // Usuário autenticado
        var principal = new ClaimsPrincipal(identity);
        return new AuthenticationState(principal);
    }

    private IEnumerable<Claim> ParseClaimsFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwt = tokenHandler.ReadJwtToken(token);

        return jwt.Claims;
    }
}