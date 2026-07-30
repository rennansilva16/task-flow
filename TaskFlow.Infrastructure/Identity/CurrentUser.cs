using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TaskFlow.Application.Identity;

namespace TaskFlow.Infrastructure.Identity;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public long Id => long.Parse(GetClaimValue(ClaimTypes.NameIdentifier));

    public string Name => GetClaimValue(ClaimTypes.Name);

    public string Login => GetClaimValue("login");

    private string GetClaimValue(string claimType)
    {
        var claim = _httpContextAccessor.HttpContext?.User.FindFirst(claimType);

        if (claim is null || string.IsNullOrWhiteSpace(claim.Value))
        {
            throw new CurrentUserClaimNotFoundException(claimType);
        }

        return claim.Value;
    }
}