using System.Security.Claims;

namespace TaskFlow.Web.Client.Authentication;

public interface IJwtParser
{
    ClaimsPrincipal CreateClaimsPrincipal(string token);
}