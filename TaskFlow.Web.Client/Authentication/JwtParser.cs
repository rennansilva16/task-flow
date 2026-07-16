using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TaskFlow.Web.Client.Authentication;

public class JwtParser : IJwtParser
{
    private const string AuthenticationType = "jwt"; // Tipo de autenticação, pode ser "jwt" ou outro valor que você preferir
    public ClaimsPrincipal CreateClaimsPrincipal(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var claims = jwtToken.Claims;
        var identity = new ClaimsIdentity(claims, AuthenticationType);
        return new ClaimsPrincipal(identity);
    }
}