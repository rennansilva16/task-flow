using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskFlow.Application.Identity;

namespace TaskFlow.Infrastructure.Identity;

public class JwtService : IJwtService
{
     private readonly JwtOptions _jwtOptions;

    public JwtService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string GenerateToken(Usuario usuario)
    {
        var claims = CreateClaims(usuario);
        var credentials = CreateSigningCredentials();
        var token = CreateJwtSecurityToken(claims, credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private Claim[] CreateClaims(Usuario usuario)
    {
        return
        [
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim("login", usuario.Login)
        ];
    }

    private SigningCredentials CreateSigningCredentials()
    {
        var keyBytes = Encoding.UTF8.GetBytes(_jwtOptions.Key);

        var securityKey = new SymmetricSecurityKey(keyBytes);

        return new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256);
    }

    private JwtSecurityToken CreateJwtSecurityToken(
        IEnumerable<Claim> claims,
        SigningCredentials credentials)
    {
        return new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes),
            signingCredentials: credentials);
    }    
}